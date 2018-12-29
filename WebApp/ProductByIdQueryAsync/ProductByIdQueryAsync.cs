using MediatR;

namespace WebApp
{
    public class ProductByIdQueryAsync : IRequest<ProductByIdQueryAsyncResult>
    {
        public long Id { get; set; }
    }
}
