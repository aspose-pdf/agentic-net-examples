using System;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class PdfGenerator
{
    // Processes all XML files in the input directory and creates a PDF for each file.
    // The method is thread‑safe: each iteration works with its own local variables.
    public static void ProcessXmlFiles(string inputDirectory, string outputDirectory)
    {
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Enumerate XML files.
        string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml", SearchOption.TopDirectoryOnly);

        // Parallel processing for high‑throughput environments.
        ParallelOptions options = new ParallelOptions
        {
            // Adjust as needed for the target environment.
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        Parallel.ForEach(xmlFiles, options, xmlPath =>
        {
            try
            {
                // Load XML document.
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                // Extract title (fallback to file name if missing).
                string title = xmlDoc.SelectSingleNode("//Title")?.InnerText ?? Path.GetFileNameWithoutExtension(xmlPath);

                // Extract item nodes.
                XmlNodeList itemNodes = xmlDoc.SelectNodes("//Item");

                // Create a new PDF document.
                using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document())
                {
                    // Add a page.
                    Aspose.Pdf.Page page = pdfDoc.Pages.Add();

                    // Create a title text fragment.
                    Aspose.Pdf.Text.TextFragment titleFragment = new Aspose.Pdf.Text.TextFragment(title);
                    // Modify the existing TextState instead of assigning a new one.
                    titleFragment.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
                    titleFragment.TextState.FontSize = 18;
                    titleFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                    // Position the title near the top of the page.
                    titleFragment.Position = new Aspose.Pdf.Text.Position(0, 800);
                    page.Paragraphs.Add(titleFragment);

                    // Create a table with two columns.
                    Aspose.Pdf.Table table = new Aspose.Pdf.Table
                    {
                        // Define column widths (in points).
                        ColumnWidths = "200 200"
                    };

                    // Header row.
                    Aspose.Pdf.Row headerRow = table.Rows.Add();
                    headerRow.Cells.Add("Name");
                    headerRow.Cells.Add("Value");

                    // Data rows.
                    foreach (XmlNode itemNode in itemNodes)
                    {
                        string name = itemNode.SelectSingleNode("Name")?.InnerText ?? string.Empty;
                        string value = itemNode.SelectSingleNode("Value")?.InnerText ?? string.Empty;

                        Aspose.Pdf.Row dataRow = table.Rows.Add();
                        dataRow.Cells.Add(name);
                        dataRow.Cells.Add(value);
                    }

                    // Add the table to the page.
                    page.Paragraphs.Add(table);

                    // Save the PDF.
                    string outputFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";
                    string outputPath = Path.Combine(outputDirectory, outputFileName);
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Processed '{Path.GetFileName(xmlPath)}' -> PDF created.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(xmlPath)}': {ex.Message}");
            }
        });
    }

    // Example entry point.
    static void Main()
    {
        string inputDir = @"C:\XmlInput";
        string outputDir = @"C:\PdfOutput";

        ProcessXmlFiles(inputDir, outputDir);
    }
}
