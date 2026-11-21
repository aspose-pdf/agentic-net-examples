using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF (could be any source, e.g., received from a web request)
        const string sourcePdfPath = "source.pdf";

        // Verify the source PDF exists before attempting to read it
        if (!File.Exists(sourcePdfPath))
        {
            Console.WriteLine($"Source PDF not found: {Path.GetFullPath(sourcePdfPath)}");
            return;
        }

        // Load the source PDF into a memory stream (simulating in‑memory input)
        byte[] pdfBytes = File.ReadAllBytes(sourcePdfPath);
        using (MemoryStream inputStream = new MemoryStream(pdfBytes))
        {
            // Load the PDF document from the memory stream
            using (Document pdfDoc = new Document(inputStream))
            {
                // Create the AutoFiller facade
                using (AutoFiller filler = new AutoFiller())
                {
                    // Bind the loaded PDF document to the filler
                    filler.BindPdf(pdfDoc);

                    // -----------------------------------------------------------------
                    // Prepare data to fill the form fields.
                    // The DataTable column names must exactly match the PDF field
                    // PartialName values. In this example the PDF contains fields named
                    // "FirstName", "LastName" and "Email".
                    // -----------------------------------------------------------------
                    DataTable table = new DataTable("FormData");
                    table.Columns.Add("FirstName", typeof(string));
                    table.Columns.Add("LastName",  typeof(string));
                    table.Columns.Add("Email",     typeof(string));

                    // Example rows – replace with real data source
                    table.Rows.Add("John", "Doe", "john.doe@example.com");

                    // Import the DataTable into the filler. All columns are strings, so
                    // no additional formatting is required.
                    filler.ImportDataTable(table);

                    // Save the filled PDF to an output memory stream (no intermediate file)
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        filler.Save(outputStream);

                        // At this point the filled PDF is in outputStream.
                        // For demonstration, write it to a file; in real scenarios you could
                        // return the byte[] directly (e.g., as an HTTP response).
                        File.WriteAllBytes("filled_output.pdf", outputStream.ToArray());
                    }
                }
            }
        }

        Console.WriteLine("PDF form filled and saved to 'filled_output.pdf'.");
    }
}

/*
 * NOTE:
 * The original build produced NuGet warnings (NU1902, NU1903) for the
 * 'System.Security.Cryptography.Pkcs' package version 9.0.0, which has known
 * vulnerabilities. To resolve these warnings, update the package to a newer,
 * non‑vulnerable version (e.g., 9.0.1 or later) in the project file:
 *
 *   <PackageReference Include="System.Security.Cryptography.Pkcs" Version="9.0.1" />
 *
 * Alternatively, you can suppress the warnings by adding the following to the
 * .csproj file:
 *
 *   <PropertyGroup>
 *       <NoWarn>NU1902;NU1903</NoWarn>
 *   </PropertyGroup>
 *
 * Updating the package is the recommended approach to keep the application
 * secure.
 */