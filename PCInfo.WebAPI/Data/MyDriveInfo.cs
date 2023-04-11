namespace PCInfo.WebAPI.Data
{
    public class MyDriveInfo
    {
        public int Id { get; set; }

        public string? Name { get; set; } // [Toshiba, A-Data, Samsung, Patriot]
        public string? DriveFormat { get; set; } // 
        public int? TotalSize { get; set; } // [512, 1024, 1536, 2048]
        public int? AvailableFreeSpace { get; set; } // rand(100, TotalSize)


        public int? MyPCInfoId { get; set; }

        #region navigation Properties

        public virtual MyPCInfo? MyPCInfo { get; set; }

        #endregion
    }
}
