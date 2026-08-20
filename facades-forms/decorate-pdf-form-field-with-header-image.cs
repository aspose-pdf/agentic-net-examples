using System;
using System.IO;
using System.Drawing;               // Required for FormFieldFacade colors
using Aspose.Pdf.Facades;          // Facade APIs

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";        // source PDF
        const string outputPdf  = "output.pdf";       // result PDF
        const string fieldName  = "Header";           // field to decorate
        const string bgImage    = "header_bg.jpg";    // background image file

        // Validate files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bgImage))
        {
            Console.Error.WriteLine($"Background image not found: {bgImage}");
            return;
        }

        // -------------------------------------------------
        // 1. Decorate the form field using FormEditor
        // -------------------------------------------------
        // FormEditor works directly on the source and writes to the target file.
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Set visual attributes via FormFieldFacade.
        formEditor.Facade = new FormFieldFacade();
        formEditor.Facade.Alignment = FormFieldFacade.AlignCenter;   // center text
        // FormFieldFacade does not support an image background directly.
        // As a placeholder we set a background color; the actual image will be added next.
        formEditor.Facade.BackgroundColor = Color.White;

        // Apply the visual settings to the specified field.
        formEditor.DecorateField(fieldName);
        formEditor.Save();   // persist changes to outputPdf

        // -------------------------------------------------
        // 2. Add a background image to the page header
        // -------------------------------------------------
        // PdfFileStamp can place an image as a header on every page.
        // It works on the PDF produced by the previous step.
        PdfFileStamp pdfStamp = new PdfFileStamp(outputPdf, outputPdf);
        pdfStamp.AddHeader(bgImage, 20f);   // 20‑point top margin
        pdfStamp.Close();                  // writes the stamp and closes the facade

        Console.WriteLine("Field 'Header' decorated with centered text and background image.");
    }
}