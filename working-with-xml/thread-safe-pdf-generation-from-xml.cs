using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf; // Document and XmlLoadOptions are in this namespace

class Program
{
    static void Main()
    {
        // Directory containing source XML files
        const string inputDirectory = "InputXml";
        // Directory where generated PDFs will be placed
        const string outputDirectory = "OutputPdf";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Verify that the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory '{inputDirectory}' does not exist. No files to process.");
            return;
        }

        // Get all XML files to process
        string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml");

        // If there are no XML files, inform the user and exit
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine($"No XML files found in '{inputDirectory}'.");
            return;
        }

        // Process each file in parallel – each iteration works with its own Document instance
        Parallel.ForEach(xmlFiles, xmlPath =>
        {
            try
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
                string pdfPath = Path.Combine(outputDirectory, fileNameWithoutExt + ".pdf");

                // Load the XML into a PDF document using the correct load options
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                // Ensure deterministic disposal of the Document
                using (Document pdfDocument = new Document(xmlPath, loadOptions))
                {
                    // Save the generated PDF
                    pdfDocument.Save(pdfPath);
                }

                Console.WriteLine($"PDF generated: {pdfPath}");
            }
            catch (Exception ex)
            {
                // Log the exception but allow other files to continue processing
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        });
    }
}
