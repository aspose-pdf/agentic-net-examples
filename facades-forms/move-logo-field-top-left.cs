// Program.cs
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Desired size of the field
        const float fieldWidth = 150f;
        const float fieldHeight = 50f;

        // Load the document
        Document doc = new Document(inputPath);
        // PageInfo.Height returns double – cast to float for the API that expects float
        float pageHeight = (float)doc.Pages[1].PageInfo.Height;

        // Coordinates for top‑left placement (origin is bottom‑left)
        float llx = 0f;                         // left
        float lly = pageHeight - fieldHeight;   // lower‑left Y
        float urx = fieldWidth;                 // right
        float ury = pageHeight;                 // upper‑right Y

        // Move the field using the non‑obsolete FormEditor constructor
        using (FormEditor formEditor = new FormEditor(doc))
        {
            bool moved = formEditor.MoveField("Logo", llx, lly, urx, ury);
            if (!moved)
            {
                Console.Error.WriteLine("Failed to move field 'Logo'.");
            }

            // Save the modified document to the desired output path
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field 'Logo' moved to top‑left corner and saved as '{outputPath}'.");
    }
}

/*
 * AsposePdfApi.csproj (updated)
 * ------------------------------------------------------------
 * The original project referenced Microsoft.Extensions.Logging.Abstractions
 * version 9.0.0, which is not available on the NuGet feed used by the
 * build environment.  The reference has been changed to the latest
 * stable version (8.0.0) that is guaranteed to exist.  No code changes are
 * required because the sample does not use any logging APIs.
 * ------------------------------------------------------------
 */

/*
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Aspose.PDF" Version="23.12" />
    <!-- Updated logging package to a version that exists -->
    <PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="8.0.0" />
  </ItemGroup>

</Project>
*/