using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF with form fields
        const string xmlPath   = "data.xml";    // XML containing form data
        const string outputPath = "filled.pdf"; // destination PDF after import

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        try
        {
            // Initialize the Form facade on the source PDF
            using (Form form = new Form(pdfPath))
            {
                // Import the XML data into the PDF form fields
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportXml(xmlStream);
                }

                // Validate required fields – log any that are empty
                foreach (string fieldName in form.FieldNames)
                {
                    if (form.IsRequiredField(fieldName))
                    {
                        string fieldValue = form.GetField(fieldName);
                        if (string.IsNullOrWhiteSpace(fieldValue))
                        {
                            Console.WriteLine($"Required field '{fieldName}' is empty.");
                        }
                    }
                }

                // Save the updated PDF to a new file
                form.Save(outputPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}