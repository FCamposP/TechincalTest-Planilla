
namespace Planilla.DTO.Others
{
    public class ResultWrapperDTO<T>
    {
        public T Data { get; set; }
        public ExceptionDTO? Exception { get; set; }
    }
}
