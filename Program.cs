using FastReport;

using var report = new Report();
report.Load("demo.frx");

// update connection from the report

var jsonConnection = report.Dictionary.FindByName("MyConnection") as FastReport.Data.DataConnectionBase;
if (jsonConnection == null)
    throw new KeyNotFoundException("MyConnection");
Console.WriteLine(jsonConnection.ConnectionString);
var builder = new FastReport.Data.JsonConnection.JsonDataSourceConnectionStringBuilder(jsonConnection.ConnectionString);
builder.Json = "https://raw.githubusercontent.com/Detrav/fros-json-demo/main/todos.json";
jsonConnection.ConnectionString = builder.ConnectionString;

// now can process the report

report.Prepare();
using var export = new FastReport.Export.Image.ImageExport();
export.Export(report, "demo.png");