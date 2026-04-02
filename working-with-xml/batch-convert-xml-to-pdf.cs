using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder that contains the XML files.
        // If a folder path is supplied as a command‑line argument it is used; otherwise the current directory is used.
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            folderPath = Directory.GetCurrentDirectory();
        }

        // Retrieve all XML files in the folder.
        string[] xmlFiles = Directory.GetFiles(folderPath, "*.xml");

        foreach (string xmlFilePath in xmlFiles)
        {
            // Load the XML file with default XmlLoadOptions.
            XmlLoadOptions loadOptions = new XmlLoadOptions();

            using (Document pdfDocument = new Document(xmlFilePath, loadOptions))
            {
                // Create a simple PDF file name (no directory part).
                string pdfFileName = Path.GetFileNameWithoutExtension(xmlFilePath) + ".pdf";
                pdfDocument.Save(pdfFileName);
                Console.WriteLine("Converted: " + pdfFileName);
            }
        }
    }
}
