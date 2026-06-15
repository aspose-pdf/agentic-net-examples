using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

// Input PDF containing an existing text box field
const string inputPdf = "input.pdf";
// Temporary PDF after filling the field
const string filledPdf = "filled.pdf";
// Final PDF with justified text
const string outputPdf = "justified_output.pdf";
// Fully qualified name of the text box field
const string fieldName = "myForm[0].TextField[0]";

if (!File.Exists(inputPdf))
{
    Console.Error.WriteLine($"Input file not found: {inputPdf}");
    return;
}

// ------------------------------------------------------------
// Step 1: Fill the text box field using the Form facade
// ------------------------------------------------------------
Form formFiller = new Form(inputPdf);
formFiller.FillField(fieldName, "This is a sample paragraph that will be justified inside the text box field. " +
                                 "It demonstrates how to set text alignment using FormEditor.");
formFiller.Save(filledPdf);
formFiller.Close(); // Release resources

// ------------------------------------------------------------
// Step 2: Apply justified alignment using FormEditor
// ------------------------------------------------------------
FormEditor editor = new FormEditor(filledPdf, outputPdf);

// Configure visual attributes via FormFieldFacade
editor.Facade = new FormFieldFacade();
editor.Facade.Alignment = FormFieldFacade.AlignJustified; // Justify text

// Apply the decoration to the specific field
editor.DecorateField(fieldName);

// Save the final PDF
editor.Save();
editor.Close(); // Release resources

Console.WriteLine($"Justified PDF saved to '{outputPdf}'.");