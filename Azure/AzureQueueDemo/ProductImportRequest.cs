namespace AzureQueueDemo
{
    public class ProductImportRequest
    {
        public ImportAction ImportAction { get; set; }

        public EnqueuedProduct Product { get; set; }
    }
}