using System;
using System.IO;
using System.Drawing; // Needed for Color types
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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the existing PDF to the FormEditor facade
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // Create a facade that defines the visual appearance
            formEditor.Facade = new FormFieldFacade();

            // Set custom colors (System.Drawing.Color is required for FormFieldFacade)
            formEditor.Facade.BackgroundColor = System.Drawing.Color.LightGray;
            formEditor.Facade.BorderColor     = System.Drawing.Color.DarkBlue;
            formEditor.Facade.TextColor       = System.Drawing.Color.Black;

            // Set font style – use a standard font name and size
            formEditor.Facade.CustomFont = "Helvetica";
            formEditor.Facade.FontSize   = 12;

            // Apply the appearance to the specific field named "CustomerName"
            formEditor.DecorateField("CustomerName");

            // Save the modified PDF to the destination file
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field \"CustomerName\" decorated and saved to '{outputPdf}'.");
    }
}
