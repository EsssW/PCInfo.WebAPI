namespace PCInfo.WebAPI.Data
{
    public class MyPCInfoDTO
    {
        // SYSTEM INFO
        public string? PCIPV4 { get; set; }
        public string? PCName { get; set; }
        public string? OSVersion { get; set; }
        public string? SystemBitRate { get; set; }
        public string? SystemCatalogPath { get; set; }
        // CPU INFO
        public string? CPUModel { get; set; }
        public string? CPUName { get; set; }
        public string? CPUManufacturerer { get; set; }
        public string? CPUNMaxClockSpeed { get; set; }
        public int CPUKernelCount { get; set; }
        // PC DRIVE INFO
        public int DriveCount { get; set; } = 0;
        // RAM INFO
        public int RAMCount { get; set; }
        public string? TotalRAM { get; set; }
        // PC Screen Resolution
        public int ScreenCount { get; set; }
        public string? HResol { get; set; }
        public string? WResol { get; set; }
        // PC VIDEO CARD
        public string? VideoCardName { get; set; } = null;
        public string? VideoCardMemoryAmount { get; set; } = null;
        public string? HDDName { get; set; }
        public string? HDDSize { get; set; }

        public List<MyDriveInfoDTO> DriveInfos { get; set; } = default!;

    }
}
