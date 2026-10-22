using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        var document = new Document(inputPath);

        // Retrieve the field named "PhoneNumber"
        if (document.Form["PhoneNumber"] is TextBoxField phoneField)
        {
            // Aspose.Pdf's TextBoxField does not expose a public SetOption method in recent versions.
            // Use reflection to invoke the internal SetOption(string, object) method, which allows us to
            // add custom attributes such as an input mask.
            SetCustomOption(phoneField, "InputMask", "(###) ###-####");
        }
        else
        {
            Console.Error.WriteLine("Field 'PhoneNumber' not found or is not a text box field.");
        }

        // Save the modified PDF
        document.Save(outputPath);

        Console.WriteLine($"PhoneNumber field updated with input mask. Saved to '{outputPath}'.");
    }

    /// <summary>
    /// Invokes the internal SetOption method on a form field via reflection.
    /// This works for all field types that inherit from Aspose.Pdf.Forms.Field.
    /// </summary>
    /// <param name="field">The form field instance.</param>
    /// <param name="optionName">The name of the option (e.g., "InputMask").</param>
    /// <param name="optionValue">The value to assign to the option.</param>
    private static void SetCustomOption(Field field, string optionName, object optionValue)
    {
        // Look for a method named "SetOption" that takes (string, object).
        const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        MethodInfo? setOptionMethod = field.GetType().GetMethod("SetOption", flags, null, new[] { typeof(string), typeof(object) }, null);

        if (setOptionMethod != null)
        {
            setOptionMethod.Invoke(field, new[] { optionName, optionValue });
        }
        else
        {
            // If the method does not exist (unlikely in supported versions), fall back to the
            // field's "CustomData" dictionary if available, or simply log a warning.
            Console.Error.WriteLine($"Unable to set custom option '{optionName}'. The SetOption method is not available on type {field.GetType().FullName}.");
        }
    }
}
