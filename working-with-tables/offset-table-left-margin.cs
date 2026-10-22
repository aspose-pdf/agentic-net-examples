using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string asposeDll = "Aspose.Pdf.dll";

        // Verify that the Aspose.PDF assembly is available at runtime.
        string asposeDllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, asposeDll);
        if (!File.Exists(asposeDllPath))
        {
            Console.Error.WriteLine($"Required assembly not found: {asposeDllPath}\nPlease ensure Aspose.PDF for .NET is referenced and the DLL is present.");
            return;
        }

        // Verify the input PDF exists before constructing the Document.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Create a table and offset it from the left margin (e.g., 50 points)
                Table table = new Table();
                table.Margin.Left = 50; // points

                // Add a header row
                Row header = table.Rows.Add();
                header.Cells.Add("Header 1");
                header.Cells.Add("Header 2");

                // Add a data row
                Row data = table.Rows.Add();
                data.Cells.Add("Value A");
                data.Cells.Add("Value B");

                // Insert the table into the first page
                Page page = doc.Pages[1];
                page.Paragraphs.Add(table);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Table with left offset saved to '{outputPath}'.");
        }
        catch (FileNotFoundException fnfEx) when (fnfEx.Message.Contains("Aspose.Pdf.dll"))
        {
            Console.Error.WriteLine("Aspose.PDF assembly could not be loaded. Ensure the Aspose.PDF for .NET library is referenced and the DLL is present.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
