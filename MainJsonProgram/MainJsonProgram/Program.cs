using Newtonsoft.Json;
using System;

namespace MainJsonProgram
{

    public static class vCardExtention
    {
        public static void ToVcard(this vCard vcard,int i)
        {
            string MainvCard = $"""
                BEGIN:VCARD
                VERSION:4.1
                FullName:{vcard.FullName}
                Name:{vcard.FirstName}
                LastName:{vcard.LastName}
                Title:{vcard.Title}
                Gender:{vcard.Gender}
                Age:{vcard.Age}
                BirthDay:{vcard.BirthDay}
                Phone:{vcard.Phone}
                Email:{vcard.Email}
                Country:{vcard.Country}
                City:{vcard.City}
                Street:{vcard.Street}
                END:VCARD
                """;
            string path = @"C:\Users\Lenovo\Desktop\Code Acadamy\MainJsonProgram\MainJsonProgram\VCards\"+i.ToString()+".vcf";
            File.WriteAllText(path, MainvCard);

        }
    }

    internal class Program
    {
        HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.GoTodoItems();
        }
        private async Task GoTodoItems()
        {
            string response = await client.GetStringAsync("https://randomuser.me/api?results=5");
            //Console.WriteLine(response);
            var root = JsonConvert.DeserializeObject<Root>(response);
            int i = 0;
            foreach (var person in root.results)
            {
                i++;
                var vCard = new vCard
                {
                    FullName = $"{person.name.title} {person.name.first} {person.name.last}",
                    Title = person.name.title,
                    FirstName = person.name.first,
                    LastName = person.name.last,
                    BirthDay = person.dob.date,
                    Age = person.dob.age,
                    Street = person.location.street.name,
                    Phone = person.phone,
                    Email = person.email,
                    City = person.location.city,
                    Country = person.location.country,
                    Gender = person.gender
                };

                vCard.ToVcard(i);

                // Extension method => vCard.ToVCard
            }
        }    
    }


    public record vCard
    {

        public string FullName { get; init; }
        public string Title { get; init; }

        public string FirstName { get; init; }  
        public string LastName { get; init; }
        public DateTime BirthDay { get; init; }
        public int Age { get; init; }
        public string Street { get; init; }
        public string Phone { get; init; }
        public string Email { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
        public string Gender { get; init; }
    }




    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Coordinates
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class Dob
    {
        public DateTime date { get; set; }
        public int age { get; set; }
    }

    public class Id
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Info
    {
        public string seed { get; set; }
        public int results { get; set; }
        public int page { get; set; }
        public string version { get; set; }
    }

    public class Location
    {
        public Street street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public object postcode { get; set; }
        public Coordinates coordinates { get; set; }
        public Timezone timezone { get; set; }
    }

    public class Login
    {
        public string uuid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string md5 { get; set; }
        public string sha1 { get; set; }
        public string sha256 { get; set; }
    }

    public class Name
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Picture
    {
        public string large { get; set; }
        public string medium { get; set; }
        public string thumbnail { get; set; }
    }

    public class Registered
    {
        public DateTime date { get; set; }
        public int age { get; set; }
    }

    public class Result
    {
        public string gender { get; set; }
        public Name name { get; set; }
        public Location location { get; set; }
        public string email { get; set; }
        public Login login { get; set; }
        public Dob dob { get; set; }
        public Registered registered { get; set; }
        public string phone { get; set; }
        public string cell { get; set; }
        public Id id { get; set; }
        public Picture picture { get; set; }
        public string nat { get; set; }
    }

    public class Root
    {
        public List<Result> results { get; set; }
        public Info info { get; set; }
    }

    public class Street
    {
        public int number { get; set; }
        public string name { get; set; }
    }

    public class Timezone
    {
        public string offset { get; set; }
        public string description { get; set; }
    }



}
    
