using WebAppServer.V1.Contracts;

namespace WebAppServer.Tests.Infrastructure.Factories;

public static class Factory
{
    public static class Arrival
    {
        public static class Response
        {
            public static Page<ArrivalResponseContract> Create(Action<Page<ArrivalResponseContract>> setup = null)
            {
                var response = new Page<ArrivalResponseContract>
                {
                    TotalItems = 1,
                    TotalPages = 1,
                    CurrentPage = 1,
                    ItemsPerPage = 1,
                    Items = new List<ArrivalResponseContract>()
                };

                setup?.Invoke(response);

                return response;
            }
        }
    }
}
