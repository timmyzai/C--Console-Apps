using System.Text.RegularExpressions;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;

namespace GraphqlNamespace
{
    [Route("testing/[controller]/[action]")]
    public class GraphqlConsumeService : Controller
    {
        private readonly string allQueries;

        public GraphqlConsumeService()
        {
            this.allQueries = System.IO.File.ReadAllText("../../Grahpql/consume/Graphql/Query.graphql");
        }
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBook()
        {
            try
            {
                {
                    var graphQLEndpoint = "https://localhost:8888/graphql/";
                    var graphQLHttpClient = new GraphQLHttpClient(graphQLEndpoint, new NewtonsoftJsonSerializer());
                    var query = ExtractGraphqlQueryByName(GraphqlQueries.GetBookDataQuery);
                    var graphQlQuery = new GraphQLRequest() { Query = query };

                    var response = await graphQLHttpClient.SendQueryAsync<MultipleBooksQueryResponse>(graphQlQuery);

                    return response.Data.Books;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IEnumerable<Book>> CreateBook(AddBookDto input)
        {
            try
            {
                {
                    var graphQLEndpoint = "https://localhost:7184/graphql/";
                    var graphQLHttpClient = new GraphQLHttpClient(graphQLEndpoint, new NewtonsoftJsonSerializer());
                    var query = ExtractGraphqlQueryByName(GraphqlQueries.AddBookQuery);
                    query = query.Replace("authorString", input.Author);
                    query = query.Replace("titleString", input.Title);
                    var graphQlQuery = new GraphQLRequest() { Query = query };

                    var response = await graphQLHttpClient.SendQueryAsync<MultipleBooksQueryResponse>(graphQlQuery);

                    return response.Data.Books;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string ExtractGraphqlQueryByName(string queryName)
        {
            var pattern = $"query {queryName}\\s*{{([\\s\\S]*?\\n}})";
            var queryMatch = Regex.Match(allQueries, pattern);

            if (queryMatch.Success)
            {
                return Regex.Replace(queryMatch.Value, @"\s+", " ").Trim();
            }
            else
            {
                throw new Exception($"Query with name '{queryName}' not found.");
            }
        }
    }
}
