using System;
using System.IO;
using Aspose.Pdf;

class ReplacePdfPageWithHtml
{
    static void Main()
    {
        // Input files
        const string pdfPath   = "source.pdf";   // Existing PDF
        const string htmlPath  = "newpage.html"; // HTML to convert to PDF page
        const int    pageIndex = 2;              // 1‑based index of page to replace
        const string outputPath = "result.pdf";

        // Validate inputs
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the original PDF
            using (Document srcDoc = new Document(pdfPath))
            {
                // Convert HTML to a temporary PDF document
                Document htmlDoc;
                try
                {
                    // HTML conversion requires GDI+ (Windows only)
                    htmlDoc = new Document(htmlPath, new HtmlLoadOptions());
                }
                catch (TypeInitializationException)
                {
                    Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation aborted.");
                    return;
                }

                // Ensure the source PDF has enough pages
                if (pageIndex < 1 || pageIndex > srcDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {pageIndex} is out of range. PDF has {srcDoc.Pages.Count} pages.");
                    return;
                }

                // Get the first page from the HTML‑generated PDF
                // (If the HTML produces multiple pages, you can adjust the index accordingly)
                Aspose.Pdf.Page newPage = htmlDoc.Pages[1];

                // Remove the page to be replaced from the source PDF
                srcDoc.Pages.Delete(pageIndex);

                // Insert the new page at the same position
                srcDoc.Pages.Insert(pageIndex, newPage);

                // Save the modified PDF
                srcDoc.Save(outputPath);
            }

            Console.WriteLine($"Page {pageIndex} replaced successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

/*
 * NOTE: To resolve the original MSBuild error ("AsposePdfApi.csproj.nuget.g.targets" not found),
 * ensure the project file uses a PackageReference for Aspose.PDF and that NuGet packages are restored.
 * Example .csproj snippet:
 *
 * <Project Sdk="Microsoft.NET.Sdk">
 *   <PropertyGroup>
 *     <TargetFramework>net6.0</TargetFramework>
 *   </PropertyGroup>
 *   <ItemGroup>
 *     <PackageReference Include="Aspose.PDF" Version="23.12.0" />
 *   </ItemGroup>
 * </Project>
 *
 * After adding the reference, run `dotnet restore` before building.
 */