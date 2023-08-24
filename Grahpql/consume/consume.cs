using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;

namespace GraphqlNamespace
{
    [Route("testing/[controller]/[action]")]
    public class GraphqlConsumeService : Controller
    {
        private readonly string allQueries = System.IO.File.ReadAllText("./Grahpql/consume/Graphql/Query.graphql").Replace("\n", " ");
        private readonly string graphQLEndpoint = "https://localhost:8888/graphql/";

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBook()
        {
            try
            {
                {
                    var graphQLHttpClient = new GraphQLHttpClient(graphQLEndpoint, new NewtonsoftJsonSerializer());
                    var query = ExtractGraphqlQueryByName(GraphqlActionType.Query, GraphqlMethodName.GetBookDataQuery);
                    var graphQlQuery = new GraphQLRequest() { Query = query };

                    var response = await graphQLHttpClient.SendQueryAsync<BooksListResponse>(graphQlQuery);

                    return response.Data.books;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public async Task<Book> CreateBook([FromBody] BookInput input)
        {
            try
            {
                {
                    var graphQLHttpClient = new GraphQLHttpClient(graphQLEndpoint, new NewtonsoftJsonSerializer());
                    var query = ExtractGraphqlQueryByName(GraphqlActionType.Mutation, GraphqlMethodName.AddBookQuery);
                    var graphQlQuery = new GraphQLRequest() { Query = query, Variables = input };
                    var response = await graphQLHttpClient.SendMutationAsync<BookResponse>(graphQlQuery);

                    return response.Data.addBook;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string ExtractGraphqlQueryByName(string actionName, string queryName)
        {
            string startMarker = $"{actionName} {queryName}";
            int startIndex = allQueries.IndexOf(startMarker);
            if (startIndex == -1) return null;

            int endIndex = allQueries.IndexOf("}", startIndex);
            if (endIndex == -1) return null;
            int nestedBraces = 1;
            int currentIndex = endIndex + 1;
            while (nestedBraces > 0 && currentIndex < allQueries.Length)
            {
                if (allQueries[currentIndex] == '{') nestedBraces++;
                else if (allQueries[currentIndex] == '}') nestedBraces--;
                currentIndex++;
            }

            var result = allQueries.Substring(startIndex, currentIndex - startIndex).Trim();
            return result;
        }
    }
}
