using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a hidden text box field to store the IP address.
            // The rectangle is set to zero size so the field is not visible.
            Aspose.Pdf.Rectangle hiddenRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField ipField = new TextBoxField(doc.Pages[1], hiddenRect)
            {
                Name = "UserIP"
            };

            // Add the field to the form.
            doc.Form.Add(ipField);

            // JavaScript to capture the user's IP address.
            // In a real scenario you would call a web service; here we set a placeholder value.
            string jsCode = "this.getField('UserIP').value = '127.0.0.1';";

            // Attach the JavaScript to the document's OpenAction so it runs when the PDF is opened.
            doc.OpenAction = new JavascriptAction(jsCode);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden IP field saved to '{outputPath}'.");
    }
}