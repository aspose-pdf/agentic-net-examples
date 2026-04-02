using System;
using System.IO;
using Aspose.Pdf;

namespace AsposePdfXmlWarningExample
{
    public class SimpleWarningHandler : IWarningCallback
    {
        public ReturnAction Warning(WarningInfo warningInfo)
        {
            Console.WriteLine("Parsing warning: " + warningInfo.WarningMessage);
            return ReturnAction.Continue;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            string xmlFile = "sample.xml";
            string pdfFile = "output.pdf";

            XmlLoadOptions loadOptions = new XmlLoadOptions();
            loadOptions.WarningHandler = new SimpleWarningHandler();

            try
            {
                using (Document pdfDocument = new Document(xmlFile, loadOptions))
                {
                    pdfDocument.Save(pdfFile);
                    Console.WriteLine("PDF generated successfully: " + pdfFile);
                }
            }
            catch (PdfException pdfEx)
            {
                Console.WriteLine("Failed to generate PDF: " + pdfEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}