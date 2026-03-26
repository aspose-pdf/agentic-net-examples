---
name: Facades - AcroForms
description: C# examples for Facades - AcroForms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - Facades - AcroForms

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **Facades - AcroForms** category.
This folder contains standalone C# examples for Facades - AcroForms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **Facades - AcroForms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Files in this folder
- [add-radio-button-group](./add-radio-button-group.cs)
- [add-text-field-formeditor](./add-text-field-formeditor.cs)
- [batch-export-form-data-xml](./batch-export-form-data-xml.cs)
- [batch-import-form-json](./batch-import-form-json.cs)
- [combine-pdf-json-data](./combine-pdf-json-data.cs)
- [export-form-data-json-split](./export-form-data-json-split.cs)
- [export-form-data-json](./export-form-data-json.cs)
- [export-form-data-xml-memory](./export-form-data-xml-memory.cs)
- [export-form-data-xml-to-html](./export-form-data-xml-to-html.cs)
- [export-form-fields-json](./export-form-fields-json.cs)
- [export-pdf-form-data-xml-async](./export-pdf-form-data-xml-async.cs)
- [export-pdf-form-to-fdf](./export-pdf-form-to-fdf.cs)
- [export-pdf-form-to-xfdf](./export-pdf-form-to-xfdf.cs)
- [export-pdf-form-to-xml](./export-pdf-form-to-xml.cs)
- [export-selected-form-fields-json](./export-selected-form-fields-json.cs)
- [export-selected-form-fields](./export-selected-form-fields.cs)
- [export-text-field-names](./export-text-field-names.cs)
- [fill-pdf-form-fields](./fill-pdf-form-fields.cs)
- [fill-pdf-form-from-byte-array](./fill-pdf-form-from-byte-array.cs)
- [fill-pdf-form-from-json](./fill-pdf-form-from-json.cs)
- [fill-textbox-field](./fill-textbox-field.cs)
- [form-fields-report](./form-fields-report.cs)
- [get-radio-button-value](./get-radio-button-value.cs)
- [import-export-form-utf8](./import-export-form-utf8.cs)
- [import-form-data-fdf](./import-form-data-fdf.cs)
- [import-form-data-json](./import-form-data-json.cs)
- [import-form-fields-missing-fields](./import-form-fields-missing-fields.cs)
- [import-form-fields-xfdf](./import-form-fields-xfdf.cs)
- [import-form-values-json](./import-form-values-json.cs)
- [import-form-xml](./import-form-xml.cs)
- [import-xml-form-validate](./import-xml-form-validate.cs)
- [justify-textbox](./justify-textbox.cs)
- [list-form-field-names](./list-form-field-names.cs)
- [list-form-fields-csv](./list-form-fields-csv.cs)
- [list-pdf-form-fields](./list-pdf-form-fields.cs)
- [merge-form-data-json](./merge-form-data-json.cs)
- [retrieve-radio-button-options](./retrieve-radio-button-options.cs)
- [validate-xml-import](./validate-xml-import.cs)
- [verify-radio-button-options](./verify-radio-button-options.cs)
- [xfdf-roundtrip-form](./xfdf-roundtrip-form.cs)

## Category Statistics
- Total examples: 40

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Facades.AutoFiller`
- `Aspose.Pdf.Facades.AutoFiller.BindPdf`
- `Aspose.Pdf.Facades.AutoFiller.Close`
- `Aspose.Pdf.Facades.AutoFiller.Dispose`
- `Aspose.Pdf.Facades.AutoFiller.ImportDataTable`
- `Aspose.Pdf.Facades.AutoFiller.InputFileName`
- `Aspose.Pdf.Facades.AutoFiller.InputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStream`
- `Aspose.Pdf.Facades.AutoFiller.OutputStreams`
- `Aspose.Pdf.Facades.AutoFiller.Save`
- `Aspose.Pdf.Facades.AutoFiller.UnFlattenFields`
- `Aspose.Pdf.Facades.BDCProperties`
- `Aspose.Pdf.Facades.BDCProperties.E`
- `Aspose.Pdf.Facades.BDCProperties.Lang`
- `Aspose.Pdf.Facades.BDCProperties.MCID`

### Rules
- Create AutoFiller with parameterless constructor: new AutoFiller().
- Call AutoFiller.Save() to persist changes to the output file.
- AutoFiller implements IDisposable — wrap in a using block for deterministic cleanup.
- Configure AutoFiller by setting properties: UnFlattenFields, OutputStream, OutputStreams, InputStream, InputFileName.
- Create PdfFileSanitization with parameterless constructor: new PdfFileSanitization().

### Warnings
- AutoFiller is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- PdfFileSanitization is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- FontColor is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- BDCProperties is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.
- Facade is in the Facades namespace — add 'using Aspose.Pdf.Facades;' explicitly.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for Facades - AcroForms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-03-26 | Run: `20260326_231459_dd6a75`
<!-- AUTOGENERATED:END -->
