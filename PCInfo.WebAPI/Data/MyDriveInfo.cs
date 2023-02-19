namespace PCInfo.WebAPI.Data
{
    public class MyDriveInfo
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? DriveFormat { get; set; }
        public string? TotalSize { get; set; }
        public string? AvailableFreeSpace { get; set; }


        public int? MyPCInfoId { get; set; }

        #region navigation Properties

        public virtual MyPCInfo? MyPCInfo { get; set; }

        #endregion
    }
}
