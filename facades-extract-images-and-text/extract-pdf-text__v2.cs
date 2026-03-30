using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace ExtractPdfText
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.Error.WriteLine("Usage: ExtractPdfText <pdf-file-path>");
                return;
            }

            string pdfPath = args[0];
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                return;
            }

            try
            {
                using (Document doc = new Document(pdfPath))
                {
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages.Accept(absorber);
                    string extractedText = absorber.Text;
                    Console.WriteLine(extractedText);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error extracting text: {ex.Message}");
            }
        }
    }
}
