using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input_form.pdf";
        const string outputPath = "output_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // JavaScript that sets the "Date" field to the current date (MM/dd/yyyy)
        string js = "this.getField('Date').value = util.printd('mm/dd/yyyy', new Date());";

        // Attach the script to the document's OpenAction (executed when the form is opened)
        doc.OpenAction = new JavascriptAction(js);

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}
