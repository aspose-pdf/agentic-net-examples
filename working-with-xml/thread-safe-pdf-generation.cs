using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

namespace ThreadSafePdfGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get all XML files in the current directory
            string inputDirectory = Directory.GetCurrentDirectory();
            string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml");

            // Configure parallel execution (use all logical processors)
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Parallel.ForEach(xmlFiles, parallelOptions, (xmlPath) =>
            {
                // Create a simple PDF file name based on the XML file name
                string pdfFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".pdf";

                // Each thread works with its own Document instance – this is thread‑safe
                using (Document pdfDocument = new Document())
                {
                    // Bind the XML data to the empty PDF document
                    pdfDocument.BindXml(xmlPath);

                    // Save the generated PDF (output path is a simple file name)
                    pdfDocument.Save(pdfFileName);
                }
            });
        }
    }
}