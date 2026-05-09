using Dapper;
using Microsoft.Data.SqlClient;
using MyOfficeACPD.Models;

namespace MyOfficeACPD.Repositories
{
    public class AcpdRepository : IAcpdRepository
    {
        private readonly string _connectionString;

        public AcpdRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<MyOfficeAcpd>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<MyOfficeAcpd>("SELECT * FROM MyOffice_ACPD ORDER BY ACPD_NowDateTime DESC");
        }

        public async Task<MyOfficeAcpd?> GetByIdAsync(string id)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<MyOfficeAcpd>(
                "SELECT * FROM MyOffice_ACPD WHERE ACPD_SID = @id",
                new { id });
        }

        public async Task<MyOfficeAcpd> CreateAsync(CreateAcpdRequest request)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            // Generate primary key using NEWSID stored procedure
            var sidParam = new DynamicParameters();
            sidParam.Add("@TableName", "MyOffice_ACPD");
            sidParam.Add("@ReturnSID", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 20);
            await conn.ExecuteAsync("NEWSID", sidParam, commandType: System.Data.CommandType.StoredProcedure);
            var newSid = sidParam.Get<string>("@ReturnSID").Trim();

            await conn.ExecuteAsync(@"
                INSERT INTO MyOffice_ACPD
                    (ACPD_SID, ACPD_Cname, ACPD_Ename, ACPD_Sname, ACPD_Email,
                     ACPD_Status, ACPD_Stop, ACPD_StopMemo, ACPD_LoginID, ACPD_LoginPWD,
                     ACPD_Memo, ACPD_NowDateTime, ACPD_NowID, ACPD_UPDDateTime, ACPD_UPDID)
                VALUES
                    (@ACPD_SID, @ACPD_Cname, @ACPD_Ename, @ACPD_Sname, @ACPD_Email,
                     @ACPD_Status, @ACPD_Stop, @ACPD_StopMemo, @ACPD_LoginID, @ACPD_LoginPWD,
                     @ACPD_Memo, GETDATE(), @ACPD_NowID, GETDATE(), @ACPD_NowID)",
                new
                {
                    ACPD_SID = newSid,
                    request.ACPD_Cname,
                    request.ACPD_Ename,
                    request.ACPD_Sname,
                    request.ACPD_Email,
                    request.ACPD_Status,
                    request.ACPD_Stop,
                    request.ACPD_StopMemo,
                    request.ACPD_LoginID,
                    request.ACPD_LoginPWD,
                    request.ACPD_Memo,
                    request.ACPD_NowID
                });

            return (await GetByIdAsync(newSid))!;
        }

        public async Task<bool> UpdateAsync(string id, UpdateAcpdRequest request)
        {
            using var conn = new SqlConnection(_connectionString);
            var rows = await conn.ExecuteAsync(@"
                UPDATE MyOffice_ACPD SET
                    ACPD_Cname      = @ACPD_Cname,
                    ACPD_Ename      = @ACPD_Ename,
                    ACPD_Sname      = @ACPD_Sname,
                    ACPD_Email      = @ACPD_Email,
                    ACPD_Status     = @ACPD_Status,
                    ACPD_Stop       = @ACPD_Stop,
                    ACPD_StopMemo   = @ACPD_StopMemo,
                    ACPD_LoginID    = @ACPD_LoginID,
                    ACPD_LoginPWD   = @ACPD_LoginPWD,
                    ACPD_Memo       = @ACPD_Memo,
                    ACPD_UPDDateTime = GETDATE(),
                    ACPD_UPDID      = @ACPD_UPDID
                WHERE ACPD_SID = @id",
                new
                {
                    id,
                    request.ACPD_Cname,
                    request.ACPD_Ename,
                    request.ACPD_Sname,
                    request.ACPD_Email,
                    request.ACPD_Status,
                    request.ACPD_Stop,
                    request.ACPD_StopMemo,
                    request.ACPD_LoginID,
                    request.ACPD_LoginPWD,
                    request.ACPD_Memo,
                    request.ACPD_UPDID
                });
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using var conn = new SqlConnection(_connectionString);
            var rows = await conn.ExecuteAsync(
                "DELETE FROM MyOffice_ACPD WHERE ACPD_SID = @id",
                new { id });
            return rows > 0;
        }
    }
}
