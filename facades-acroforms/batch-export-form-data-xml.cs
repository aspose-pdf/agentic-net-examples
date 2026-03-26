using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of PDF form files to process (place the files in the executable directory)
        string[] pdfFiles = new string[] { "form1.pdf", "form2.pdf", "form3.pdf" };

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF form using the Form facade
            using (Form form = new Form(pdfPath))
            {
                // Create a simple XML file name based on the PDF name
                string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";

                // Export the form fields to the XML file
                using (FileStream xmlStream = new FileStream(xmlFileName, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXml(xmlStream);
                }

                Console.WriteLine($"Exported form data to {xmlFileName}");
            }
        }
    }
}