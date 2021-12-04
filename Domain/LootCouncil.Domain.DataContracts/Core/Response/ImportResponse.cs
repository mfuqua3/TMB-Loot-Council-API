namespace LootCouncil.Domain.DataContracts.Core.Response
{
    public class ImportResponse
    {
        public int Id { get; set; }
        public bool Completed { get; set; }
        public bool Faulted { get; set; }
        public string Error { get; set; }
        public double Progress { get; set; }
        public string InitiatedBy { get; set; }
    }
}