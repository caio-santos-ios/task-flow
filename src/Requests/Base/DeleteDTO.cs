using to_do_list.src.Requests;

namespace to_do_list.src.Shared.DTOs
{
    public class DeleteDTO : Request
    {
        public string Id {get;set;} = string.Empty;
    }
}