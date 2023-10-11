// See https://aka.ms/new-console-template for more information

using rokka_client_c_sharp;
using rokka_client_c_sharp.Configuration;
using rokka_client_c_sharp.Models.MetaData;

Console.WriteLine("Hello, World!");

var configuration = new RokkaConfiguration()
{
    Organization = Environment.GetEnvironmentVariable("ROKKA_ORGANISATION") ?? string.Empty,
    Key = Environment.GetEnvironmentVariable("ROKKA_KEY") ?? string.Empty,
    RenderStack = Environment.GetEnvironmentVariable("ROKKA_RENDER_STACK") ?? string.Empty,
};

var rokkaClient = new RokkaClient(configuration);

var bytes = File.ReadAllBytes("./sample.jpg");

var metadata = new CreateMetadata
{
    MetaUser = new MetaDataUser { { "AltText", "Landscape" } },
    MetaDynamic = new MetaDataDynamic()
    {
        SubjectArea = new Area {X = 10, Y= 10, Width = 100, Height = 100},
        CropArea = new Area {X = 100, Y = 100, Width = 300, Height = 300},
        Version = new rokka_client_c_sharp.Models.MetaData.Version() { Text = "v0.0.2"}
    },
    Options = new MetaDataOptions()
    {
        Protected = true
    }
};

var options = new CreateOptions()
{
    OptimizeSource = true
};

var response = await rokkaClient.SourceImages.Create("sample.jpg", bytes, metadata, options);

Console.WriteLine(response.DetailedMessage);