using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace LearningElasticSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = 
                new ConnectionSettings(new Uri("https://ee949088a836d3d2ef4d10c862472ceb.eu-west-1.aws.found.io:9243/"))
                .DefaultIndex("people")
                .BasicAuthentication("elastic", "4KCZvEMSfZXEzamMXxjpvsOn");
            var client = new ElasticClient(settings);

            var person = new Person
            {
                Id = 1,
                FirstName = "Martijn",
                LastName = "Laarman"
            };

            var indexResponse = client.Index(person);

            Console.WriteLine(indexResponse);

            var searchResponse = client.Search<Person>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.FirstName)
                        .Query("Martijn")
                    )
                )
            );

            var people = searchResponse.Documents;

            foreach (var pers in people)
            {
                Console.WriteLine($"{pers.Id} {pers.FirstName} {pers.LastName}");
            }
        }
    }
}
