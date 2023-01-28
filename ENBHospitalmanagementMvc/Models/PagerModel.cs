using System.Collections.Generic;

namespace ENBHospitalmanagementMvc.Models
{
  public class PagerModel<T> where T : class
  {
    public IEnumerable<T> Data { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int TotalRows { get; set; }
  }
}