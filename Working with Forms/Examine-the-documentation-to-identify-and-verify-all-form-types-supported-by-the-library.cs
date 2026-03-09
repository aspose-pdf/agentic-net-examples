using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object associated with the document
            Form form = doc.Form;

            // Retrieve the form type of the loaded document
            FormType currentType = form.Type;
            Console.WriteLine($"Current document form type: {currentType} (value {(int)currentType})");

            // Enumerate all supported FormType values defined in the library
            Console.WriteLine("Supported FormType values:");
            foreach (FormType ft in Enum.GetValues(typeof(FormType)))
            {
                Console.WriteLine($"- {ft} = {(int)ft}");
            }

            // Verify that the current form type is one of the supported enum values
            bool isSupported = Enum.IsDefined(typeof(FormType), currentType);
            Console.WriteLine($"Is the current form type supported? {isSupported}");
        }
    }
}