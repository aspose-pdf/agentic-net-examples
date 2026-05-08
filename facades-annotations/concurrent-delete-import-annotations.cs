using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";                 // source PDF
        const string deleteOutputPdf = "deleted_pages.pdf"; // result of delete operation
        const string importOutputPdf = "imported_annotations.pdf"; // result of import operation
        const string xfdfFile = "sample.xfdf";               // temporary XFDF file for import

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a minimal XFDF file (empty annotations) for the import test
        File.WriteAllText(xfdfFile,
            @"<?xml version='1.0' encoding='UTF-8'?>
              <xfdf xmlns='http://ns.adobe.com/xfdf/'>
              </xfdf>");

        // Task 1: Delete pages 2 and 3 from the source PDF
        Task deleteTask = Task.Run(() =>
        {
            try
            {
                var editor = new PdfFileEditor();
                // TryDelete reads the source file and writes a new file without the specified pages.
                bool success = editor.TryDelete(inputPdf, new int[] { 2, 3 }, deleteOutputPdf);
                Console.WriteLine(success
                    ? $"Delete operation completed: {deleteOutputPdf}"
                    : "Delete operation failed.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Delete task error: {ex.Message}");
            }
        });

        // Task 2: Import annotations from an XFDF file into the same source PDF
        Task importTask = Task.Run(() =>
        {
            try
            {
                var annotEditor = new PdfAnnotationEditor();
                // Bind the same source PDF for annotation editing
                annotEditor.BindPdf(inputPdf);
                // Import (empty) annotations from the XFDF file
                annotEditor.ImportAnnotationsFromXfdf(xfdfFile);
                // Save the modified PDF to a separate file
                annotEditor.Save(importOutputPdf);
                Console.WriteLine($"Import operation completed: {importOutputPdf}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Import task error: {ex.Message}");
            }
        });

        // Wait for both operations to finish
        Task.WaitAll(deleteTask, importTask);

        // Clean up temporary XFDF file
        try { File.Delete(xfdfFile); } catch { /* ignore */ }

        Console.WriteLine("Concurrent import and delete test finished.");
    }
}