namespace MyOfficeACPD.Models
{
    public class CreateAcpdRequest
    {
        /// <example>王小明</example>
        public string? ACPD_Cname { get; set; }
        /// <example>John Wang</example>
        public string? ACPD_Ename { get; set; }
        /// <example>John</example>
        public string? ACPD_Sname { get; set; }
        /// <example>john.wang@example.com</example>
        public string? ACPD_Email { get; set; }
        /// <example>1</example>
        public byte? ACPD_Status { get; set; }
        /// <example>false</example>
        public bool? ACPD_Stop { get; set; }
        /// <example></example>
        public string? ACPD_StopMemo { get; set; }
        /// <example>john.wang</example>
        public string? ACPD_LoginID { get; set; }
        /// <example>P@ssw0rd123</example>
        public string? ACPD_LoginPWD { get; set; }
        /// <example>Test user created via API</example>
        public string? ACPD_Memo { get; set; }
        /// <example>admin</example>
        public string? ACPD_NowID { get; set; }
    }
}
