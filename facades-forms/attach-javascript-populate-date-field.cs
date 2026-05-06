using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source PDF exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (a SaveableFacade) to add a JavaScript action
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // JavaScript that sets the "Date" field to the current date (formatted as mm/dd/yyyy)
            string jsCode = "var f = this.getField('Date'); " +
                            "f.value = util.printd('mm/dd/yyyy', new Date());";

            // Attach the script to the document open event
            editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, jsCode);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPdf}'.");
    }
}