using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

#pragma warning disable NU1902 // Package 'System.Security.Cryptography.Pkcs' 9.0.0 has a known moderate severity vulnerability
#pragma warning disable NU1903 // Package 'System.Security.Cryptography.Pkcs' 9.0.0 has a known high severity vulnerability

class Program
{
    static void Main()
    {
        // Directory containing XML files
        const string inputDirectory = "XmlInputs";
        // Directory where generated PDFs will be saved
        const string outputDirectory = "GeneratedPdfs";

        // Validate input directory
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory '{inputDirectory}' does not exist. Nothing to process.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Collect all XML files in the input directory
        string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml", SearchOption.TopDirectoryOnly);
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine($"No XML files found in '{inputDirectory}'.");
            return;
        }

        Console.WriteLine($"Found {xmlFiles.Length} XML file(s) to process.");

        foreach (string xmlPath in xmlFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
            string pdfPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".pdf");

            Console.WriteLine($"\nProcessing '{xmlPath}'...");
            try
            {
                // Load XML document
                XDocument xDoc = XDocument.Load(xmlPath);
                Console.WriteLine("  XML loaded successfully.");

                // Create a new PDF document
                using (Document pdfDoc = new Document())
                {
                    Console.WriteLine("  PDF document created.");

                    // Add a page
                    Page page = pdfDoc.Pages.Add();
                    Console.WriteLine("  Page added to PDF.");

                    // Extract information from XML
                    string rootName = xDoc.Root?.Name.LocalName ?? "UnknownRoot";
                    string innerXml = xDoc.Root?.ToString() ?? string.Empty;

                    // Title fragment
                    TextFragment titleFragment = new TextFragment($"XML Root: {rootName}");
                    titleFragment.TextState.FontSize = 16;
                    titleFragment.TextState.FontStyle = FontStyles.Bold;
                    titleFragment.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
                    page.Paragraphs.Add(titleFragment);
                    Console.WriteLine("  Title added to PDF.");

                    // Content fragment (escaped for readability)
                    TextFragment contentFragment = new TextFragment(innerXml);
                    contentFragment.TextState.FontSize = 10;
                    contentFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                    page.Paragraphs.Add(contentFragment);
                    Console.WriteLine("  Content added to PDF.");

                    // Save the PDF
                    pdfDoc.Save(pdfPath);
                    Console.WriteLine($"  PDF saved to '{pdfPath}'.");
                }

                Console.WriteLine($"Successfully processed '{xmlPath}'.");
            }
            catch (Exception ex)
            {
                // Log detailed error information
                Console.Error.WriteLine($"Error processing '{xmlPath}': {ex.GetType().Name} - {ex.Message}");
                Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        Console.WriteLine("\nProcessing completed.");
    }
}
#pragma warning restore NU1902
#pragma warning restore NU1903