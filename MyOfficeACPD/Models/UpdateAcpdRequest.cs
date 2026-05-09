namespace MyOfficeACPD.Models
{
    public class UpdateAcpdRequest
    {
        /// <example>王大明（更新）</example>
        public string? ACPD_Cname { get; set; }
        /// <example>John Wang Updated</example>
        public string? ACPD_Ename { get; set; }
        /// <example>Johnny</example>
        public string? ACPD_Sname { get; set; }
        /// <example>john.updated@example.com</example>
        public string? ACPD_Email { get; set; }
        /// <example>1</example>
        public byte? ACPD_Status { get; set; }
        /// <example>false</example>
        public bool? ACPD_Stop { get; set; }
        /// <example></example>
        public string? ACPD_StopMemo { get; set; }
        /// <example>john.wang</example>
        public string? ACPD_LoginID { get; set; }
        /// <example>NewP@ssw0rd!</example>
        public string? ACPD_LoginPWD { get; set; }
        /// <example>Updated via API</example>
        public string? ACPD_Memo { get; set; }
        /// <example>admin</example>
        public string? ACPD_UPDID { get; set; }
    }
}
