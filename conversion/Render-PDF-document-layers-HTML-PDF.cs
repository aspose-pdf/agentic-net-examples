using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Resolve the directory that contains the source HTML file.
        // You can replace this with an absolute path or a configuration value.
        string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        // Input HTML and output PDF file paths.
        string htmlPath = Path.Combine(dataDir, "input.html");
        string pdfPath  = Path.Combine(dataDir, "output.pdf");

        // Verify that the source HTML file exists before attempting conversion.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: Cannot find source file '{htmlPath}'. Please ensure the file exists.");
            return;
        }

        // Load the HTML document. HtmlLoadOptions can be customized if needed.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        try
        {
            // Use a using block to ensure the Document is disposed properly.
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Convert the HTML to PDF.
                doc.Save(pdfPath);

                // After conversion, iterate through each page and list any layers present.
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // The Layers property returns a List<Layer>. It may be null or empty.
                    if (page.Layers != null && page.Layers.Count > 0)
                    {
                        Console.WriteLine($"Page {i} contains {page.Layers.Count} layer(s):");
                        foreach (Layer layer in page.Layers)
                        {
                            Console.WriteLine($"  Name: {layer.Name}, Id: {layer.Id}, Locked: {layer.Locked}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Page {i} contains no layers.");
                    }
                }
            }

            Console.WriteLine($"HTML has been rendered to PDF at: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
