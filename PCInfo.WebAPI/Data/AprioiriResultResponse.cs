namespace PCInfo.WebAPI.Data
{
    public class AprioiriResultResponse
    {
        public string Antecedent { get; set; }
        public string Consequent { get; set; }
        public double Support { get; set; }
        public double Confidence { get; set; }
    }
}
