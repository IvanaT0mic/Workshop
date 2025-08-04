namespace ProjectForTesting.Models;

public class ToolOrderStatus
{
    public int StatusId { get; set; }
    public int ChangedBy { get; set; }
    public DateTime ChangeDate { get; set; }
    public bool IsActive { get; set; }
    public int ToolOrderId { get; set; }
}