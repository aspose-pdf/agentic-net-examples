using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "UnderSignature.pdf";

        // Name of the existing signature field in the PDF.
        const string signatureFieldName = "Signature1";
        // Desired name for the new text field that will be placed under the signature.
        const string newTextFieldName = "TextUnderSignature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF as a Document to read the existing signature field.
        Document doc = new Document(inputPath);
        // Retrieve the signature field.
        SignatureField sigField = doc.Form[signatureFieldName] as SignatureField;
        if (sigField == null)
        {
            Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found.");
            return;
        }

        // Rectangle of the signature field.
        Rectangle sigRect = sigField.Rect;
        // Page index (0‑based) where the signature resides.
        int sigPageIndex = sigField.PageIndex;
        // FormEditor expects a 1‑based page number.
        int sigPageNumber = sigPageIndex + 1;

        // Define a rectangle positioned just below the signature field.
        const double verticalGap = 5; // gap between fields
        const double textFieldHeight = 30;
        Rectangle textRect = new Rectangle(
            sigRect.LLX,
            sigRect.LLY - textFieldHeight - verticalGap,
            sigRect.URX,
            sigRect.LLY - verticalGap);

        // Bind a FormEditor to the same Document and add the new text field.
        using (FormEditor editor = new FormEditor(doc))
        {
            // Add a single‑line text field.
            editor.AddField(FieldType.Text, newTextFieldName, sigPageNumber,
                (float)textRect.LLX, (float)textRect.LLY,
                (float)textRect.URX, (float)textRect.URY);

            // Set justified alignment for the newly added field (cast to int).
            editor.SetFieldAlignment(newTextFieldName, (int)HorizontalAlignment.Justify);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}
