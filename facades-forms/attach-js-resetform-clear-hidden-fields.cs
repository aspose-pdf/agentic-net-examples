using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF, add JavaScript to the ResetForm button, and save.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document into the FormEditor facade.
            formEditor.BindPdf(inputPdf);

            // JavaScript that resets the form and clears hidden fields.
            string js = @"this.resetForm(); " +
                        @"var hidden = ['HiddenField1','HiddenField2']; " +
                        @"for (var i = 0; i < hidden.length; i++) { " +
                        @"var f = this.getField(hidden[i]); " +
                        @"if (f != null) f.value = ''; }";

            // Attach the script to the button named "ResetForm".
            formEditor.AddFieldScript("ResetForm", js);

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPdf}'.");
    }
}