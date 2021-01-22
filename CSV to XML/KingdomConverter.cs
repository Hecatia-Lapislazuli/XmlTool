using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CsvHelper;
using static CSV_to_XML.Program;

namespace CSV_to_XML {
    public class KingdomConverter {
		public static void Kingdoms_CSVtoXML(string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			KingdomRecord kingdomsRecord = new KingdomRecord();
			IEnumerable<KingdomRecord> records = csv.EnumerateRecords(kingdomsRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("Kingdoms");
				foreach (KingdomRecord record in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("Kingdom");

					//Changes

					//Defaults

					//Temporary

					//Write
					writer.WriteAttributeString("id", record.id);
					if (!record.banner_key.Equals("")) writer.WriteAttributeString("banner_key", record.banner_key);
					if (!record.owner.Equals("")) writer.WriteAttributeString("owner", "Hero." + record.owner);
					if (!record.label_color.Equals("")) writer.WriteAttributeString("label_color", record.label_color);
					writer.WriteAttributeString("primary_banner_color", record.primary_banner_color);
					writer.WriteAttributeString("secondary_banner_color", record.secondary_banner_color);
					if (!record.color.Equals("")) writer.WriteAttributeString("color", record.color);
					if (!record.color2.Equals("")) writer.WriteAttributeString("color2", record.color2);
					if (!record.alternative_color.Equals("")) writer.WriteAttributeString("alternative_color", record.alternative_color);
					if (!record.alternative_color2.Equals("")) writer.WriteAttributeString("alternative_color2", record.alternative_color2);
					writer.WriteAttributeString("culture", "Culture." + record.culture);
					if (!record.settlement_banner_mesh.Equals("")) writer.WriteAttributeString("settlement_banner_mesh", record.settlement_banner_mesh);
					if (!record.flag_mesh.Equals("")) writer.WriteAttributeString("flag_mesh", record.flag_mesh);
					writer.WriteAttributeString("name", "{=Kingdoms.Kingdom." + record.id + ".name}" + record.name);

					writeLocalizationNode(localizationWriter, "Kingdoms.Kingdom." + record.id + ".name", record.name);
					//writeLocalizationNode(module_strings_writer, "str_faction_ruler." + record.id, "{=Kingdoms.Kingdom." + record.id + ".name}" + record.name);

					if (!record.short_name.Equals("")) {
						writer.WriteAttributeString("short_name", "{=Kingdoms.Kingdom." + record.id + ".short_name}" + record.short_name);
						writeLocalizationNode(localizationWriter, "Kingdoms.Kingdom." + record.id + ".short_name", record.short_name);
						//writeLocalizationNode(module_strings_writer, "."+record.id, "{=Kingdoms.Kingdom." + record.id + ".short_name}" + record.short_name);
					}

					if (!record.text.Equals("")) {
						writer.WriteAttributeString("text", "{=Kingdoms.Kingdom." + record.id + ".text}" + record.text);
						writeLocalizationNode(localizationWriter, "Kingdoms.Kingdom." + record.id + ".text", record.text);
						//writeLocalizationNode(module_strings_writer, "." + record.id, "{=Kingdoms.Kingdom." + record.id + ".text}" + record.text);
					}
					if (!record.title.Equals("")) {
						writer.WriteAttributeString("title", "{=Kingdoms.Kingdom." + record.id + ".title}" + record.title);
						writeLocalizationNode(localizationWriter, "Kingdoms.Kingdom." + record.id + ".title", record.title);
						//writeLocalizationNode(module_strings_writer, "." + record.id, "{=Kingdoms.Kingdom." + record.id + ".title}" + record.title);
					}
					if (!record.ruler_title.Equals("")) {
						writer.WriteAttributeString("ruler_title", "{=Kingdoms.Kingdom." + record.id + ".ruler_title}" + record.ruler_title);
						writeLocalizationNode(localizationWriter, "Kingdoms.Kingdom." + record.id + ".ruler_title", record.ruler_title);
						//writeLocalizationNode(module_strings_writer, "." + record.id, "{=Kingdoms.Kingdom." + record.id + ".ruler_title}" + record.ruler_title);
					}

					writer.WriteStartElement("relationships");
					if (!record.Relationships_Kingdom0_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom0_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom0_value);
						if (!record.Relationships_Kingdom0_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom0_value.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Kingdom1_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom1_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom1_value);
						if (!record.Relationships_Kingdom1_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom1_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Kingdom2_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom2_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom2_value);
						if (!record.Relationships_Kingdom2_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom2_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Kingdom3_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("kingdom", "Kingdom." + record.Relationships_Kingdom3_name);
						writer.WriteAttributeString("value", record.Relationships_Kingdom3_value);
						if (!record.Relationships_Kingdom3_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Kingdom3_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					//~~~
					if (!record.Relationships_Clan0_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan0_name);
						writer.WriteAttributeString("value", record.Relationships_Clan0_value);
						if (!record.Relationships_Clan0_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan0_value.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Clan1_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan1_name);
						writer.WriteAttributeString("value", record.Relationships_Clan1_value);
						if (!record.Relationships_Clan1_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan1_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Clan2_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan2_name);
						writer.WriteAttributeString("value", record.Relationships_Clan2_value);
						if (!record.Relationships_Clan2_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan2_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					if (!record.Relationships_Clan3_name.Equals("")) {
						writer.WriteStartElement("relationship");

						writer.WriteAttributeString("clan", "Faction." + record.Relationships_Clan3_name);
						writer.WriteAttributeString("value", record.Relationships_Clan3_value);
						if (!record.Relationships_Clan3_isAtWar.Equals("")) writer.WriteAttributeString("isAtWar", record.Relationships_Clan3_isAtWar.ToLower());

						writer.WriteEndElement();
					}
					writer.WriteEndElement();

					writer.WriteStartElement("policies");
					if (!record.Policy0.Equals("") || !record.Policy1.Equals("") || !record.Policy2.Equals("") || !record.Policy3.Equals("")) {
						if (!record.Policy0.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy0);
							writer.WriteEndElement();
						}
						if (!record.Policy1.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy1);
							writer.WriteEndElement();
						}
						if (!record.Policy2.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy2);
							writer.WriteEndElement();
						}
						if (!record.Policy3.Equals("")) {
							writer.WriteStartElement("policy");
							writer.WriteAttributeString("id", record.Policy3);
							writer.WriteEndElement();
						}
					}
					writer.WriteEndElement();

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		}
		public class KingdomRecord {
			public string id { get; set; }
			public string banner_key { get; set; }
			public string owner { get; set; }
			public string label_color { get; set; }
			public string primary_banner_color { get; set; }
			public string secondary_banner_color { get; set; }
			public string color { get; set; }
			public string color2 { get; set; }
			public string alternative_color { get; set; }
			public string alternative_color2 { get; set; }
			public string culture { get; set; }
			public string settlement_banner_mesh { get; set; }
			public string flag_mesh { get; set; }
			public string name { get; set; }
			public string short_name { get; set; }
			public string text { get; set; }
			public string title { get; set; }
			public string ruler_title { get; set; }

			public string Policy0 { get; set; }
			public string Policy1 { get; set; }
			public string Policy2 { get; set; }
			public string Policy3 { get; set; }

			public string Relationships_Kingdom0_name { get; set; }
			public string Relationships_Kingdom0_value { get; set; }
			public string Relationships_Kingdom0_isAtWar { get; set; }
			public string Relationships_Kingdom1_name { get; set; }
			public string Relationships_Kingdom1_value { get; set; }
			public string Relationships_Kingdom1_isAtWar { get; set; }
			public string Relationships_Kingdom2_name { get; set; }
			public string Relationships_Kingdom2_value { get; set; }
			public string Relationships_Kingdom2_isAtWar { get; set; }
			public string Relationships_Kingdom3_name { get; set; }
			public string Relationships_Kingdom3_value { get; set; }
			public string Relationships_Kingdom3_isAtWar { get; set; }

			public string Relationships_Clan0_name { get; set; }
			public string Relationships_Clan0_value { get; set; }
			public string Relationships_Clan0_isAtWar { get; set; }
			public string Relationships_Clan1_name { get; set; }
			public string Relationships_Clan1_value { get; set; }
			public string Relationships_Clan1_isAtWar { get; set; }
			public string Relationships_Clan2_name { get; set; }
			public string Relationships_Clan2_value { get; set; }
			public string Relationships_Clan2_isAtWar { get; set; }
			public string Relationships_Clan3_name { get; set; }
			public string Relationships_Clan3_value { get; set; }
			public string Relationships_Clan3_isAtWar { get; set; }
		}
	}
}
