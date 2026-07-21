using System;
using System.IO;
using System.Drawing; // For System.Drawing.Color
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // Source PDF containing the form
        const string outputPdf = "output.pdf"; // Destination PDF after decoration

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor does not accept a file path in the constructor in recent versions.
        // Use the parameter‑less constructor and bind the PDF explicitly.
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // Create a facade to specify visual attributes
            formEditor.Facade = new FormFieldFacade();

            // Custom appearance settings – fully qualify System.Drawing.Color to avoid
            // ambiguity with Aspose.Pdf.Color, and fully qualify the FontStyle enum.
            formEditor.Facade.BackgroundColor = System.Drawing.Color.LightGray;   // Background shade
            formEditor.Facade.BorderColor     = System.Drawing.Color.DarkBlue;    // Border color
            formEditor.Facade.TextColor       = System.Drawing.Color.Black;      // Text (font) color
            formEditor.Facade.Font            = Aspose.Pdf.Facades.FontStyle.Helvetica; // Font enum value
            formEditor.Facade.FontSize        = 12;                                 // Font size

            // Apply the appearance to the specific field named "CustomerName"
            formEditor.DecorateField("CustomerName");

            // Persist changes to the output PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field \"CustomerName\" decorated and saved to '{outputPdf}'.");
    }
}
