//
// NPCCharacterConverter.cs
//
// Author:
//       Urist_McAurelian <Discord: Urist_McAurelian#2289>
//
// Copyright (c) 2021 Urist_McAurelian
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using CsvHelper;
using static XmlTool.Program;

namespace XmlTool {
	//Functionally same result after running through twice... mostly. There are some outlier cases, but rare enough to not be a priority for the moment.
	public class NPCCharacterConverter {
#pragma warning disable CA2211 // Non-constant fields should not be visible
		public static int NeededEquipmentSets = -1;
        public static int AllowedEquipmentSets = 30;
        public static int AffectedNPCCharacters = 0;
#pragma warning restore CA2211 // Non-constant fields should not be visible

		public static void NPCCharacters_CSVtoXML(string fileInput, string fileOutput, XmlWriter localizationWriter, XmlWriter module_strings_writer) {
			StreamReader reader = new StreamReader(fileInput);
			CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

			NPCCharacterRecord npcCharacterRecord = new NPCCharacterRecord();
			IEnumerable<NPCCharacterRecord> records = csv.EnumerateRecords(npcCharacterRecord);

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(fileOutput, settings)) {
				writer.WriteStartElement("NPCCharacters");

				writeHeadderComment(writer);

				foreach (NPCCharacterRecord record in records) {
					if (record.id.Equals("TODO")) break;
					if (record.id.Equals("VANILLA")) break;
					if ("#".Equals(record.id.FirstOrDefault())) continue;
					if (record.id.Equals("")) continue;

					writer.WriteStartElement("NPCCharacter");

					//Changes
					record.is_basic_troop = record.is_basic_troop.ToLower();
					record.is_child_template = record.is_child_template.ToLower();
					record.is_companion = record.is_companion.ToLower();
					record.is_female = record.is_female.ToLower();
					record.is_hero = record.is_hero.ToLower();
					record.is_mercenary = record.is_mercenary.ToLower();
					record.is_template = record.is_template.ToLower();

					//Defaults
					//if (record.default_group.Equals("")) record.default_group = "Infantry";
					//if (record.civilianTemplate.Equals("")) record.civilianTemplate = "npc_armed_wanderer_equipment_template_empire";
					//if (record.battleTemplate.Equals("")) record.battleTemplate = "emp_bat_template_lady";
					//if (record.level.Equals("")) record.level = "15";

					//Temporary
					//if (record.age.Equals("")) record.age = "23";

					//Write
					writer.WriteAttributeString(null, "id", null, record.id);
					if (!record.default_group.Equals("")) writer.WriteAttributeString(null, "default_group", null, record.default_group);
					if (!record.level.Equals("")) writer.WriteAttributeString(null, "level", null, record.level);
					if (!record.age.Equals("")) writer.WriteAttributeString(null, "age", null, record.age);////
					if (!record.voice.Equals("")) writer.WriteAttributeString(null, "voice", null, record.voice);//
					if (!record.is_hero.Equals("")) writer.WriteAttributeString(null, "is_hero", null, record.is_hero);
					if (!record.is_female.Equals("")) writer.WriteAttributeString(null, "is_female", null, record.is_female);////
					if (!record.is_basic_troop.Equals("")) writer.WriteAttributeString(null, "is_basic_troop", null, record.is_basic_troop);//
					if (!record.is_template.Equals("")) writer.WriteAttributeString(null, "is_template", null, record.is_template);//
					if (!record.is_child_template.Equals("")) writer.WriteAttributeString(null, "is_child_template", null, record.is_child_template);//
					if (!record.culture.Equals("")) writer.WriteAttributeString(null, "culture", null, "Culture." + record.culture);
					if (!record.name.Equals("")) writer.WriteAttributeString("name", GetLocalizedString(localizationWriter, record.name, record.id, "name", "NPCCharacters.NPCCharacter"));
					if (!record.banner_symbol_mesh_name.Equals("")) writer.WriteAttributeString(null, "banner_symbol_mesh_name", null, record.banner_symbol_mesh_name);////
					if (!record.banner_symbol_color.Equals("")) writer.WriteAttributeString(null, "banner_symbol_color", null, record.banner_symbol_color);//
					if (!record.banner_key.Equals("")) writer.WriteAttributeString(null, "banner_key", null, record.banner_key);////
					if (!record.occupation.Equals("")) writer.WriteAttributeString(null, "occupation", null, record.occupation);////
					if (!record.civilianTemplate.Equals("")) writer.WriteAttributeString(null, "civilianTemplate", null, "NPCCharacter." + record.civilianTemplate);//
					if (!record.battleTemplate.Equals("")) writer.WriteAttributeString(null, "battleTemplate", null, "NPCCharacter." + record.battleTemplate);//
					if (!record.is_companion.Equals("")) writer.WriteAttributeString(null, "is_companion", null, record.is_companion);////
					if (!record.offset.Equals("")) writer.WriteAttributeString(null, "offset", null, record.offset);////
					if (!record.is_mercenary.Equals("")) writer.WriteAttributeString(null, "is_mercenary", null, record.is_mercenary);////
					if (!record.formation_position_preference.Equals("")) writer.WriteAttributeString(null, "formation_position_preference", null, record.formation_position_preference);////
					if (!record.default_equipment_set.Equals("")) writer.WriteAttributeString(null, "default_equipment_set", null, record.default_equipment_set);//
					if (!record.skill_template.Equals("")) writer.WriteAttributeString(null, "skill_template", null, "NPCCharacter." + record.skill_template);//
					if (!record.upgrade_requires.Equals("")) writer.WriteAttributeString(null, "upgrade_requires", null, "ItemCategory." + record.upgrade_requires);//

					//Face
					if (!record.Face_face_key_value.Equals("") || !record.Face_face_key_template_value.Equals("") || !record.Face_BodyProperties_version.Equals("") ||
						!record.Face_hair_tags_hair_tag0_name.Equals("") || !record.Face_hair_tags_hair_tag1_name.Equals("") || !record.Face_hair_tags_hair_tag2_name.Equals("") ||
						!record.Face_beard_tags_beard_tag0_name.Equals("") || !record.Face_beard_tags_beard_tag1_name.Equals("") || !record.Face_beard_tags_beard_tag2_name.Equals("") ||
						!record.Face_tattoo_tags_tattoo_tag0_name.Equals("") || !record.Face_tattoo_tags_tattoo_tag1_name.Equals("") || !record.Face_tattoo_tags_tattoo_tag2_name.Equals("")) {
						writer.WriteStartElement("face");

						if (!record.Face_face_key_value.Equals("")) {
							writer.WriteStartElement("face_key");

							writer.WriteAttributeString("value", record.Face_face_key_value);
							if (!record.Face_face_key_max_value.Equals("")) writer.WriteAttributeString("max_value", record.Face_face_key_max_value);

							writer.WriteEndElement();
						}

						if (!record.Face_face_key_template_value.Equals("")) {
							writer.WriteStartElement("face_key_template");
							writer.WriteAttributeString("value", "NPCCharacter." + record.Face_face_key_template_value);
							writer.WriteEndElement();
						}

						if (!record.Face_BodyProperties_version.Equals("")) {
							writer.WriteStartElement("BodyProperties");

							writer.WriteAttributeString("version", record.Face_BodyProperties_version);
							writer.WriteAttributeString("key", record.Face_BodyProperties_key);
							if (!record.Face_BodyProperties_age.Equals("")) writer.WriteAttributeString("age", record.Face_BodyProperties_age);
							if (!record.Face_BodyProperties_weight.Equals("")) writer.WriteAttributeString("weight", record.Face_BodyProperties_weight);
							if (!record.Face_BodyProperties_build.Equals("")) writer.WriteAttributeString("build", record.Face_BodyProperties_build);

							writer.WriteEndElement();

							if (!record.Face_BodyPropertiesMax_version.Equals("")) {
								writer.WriteStartElement("BodyPropertiesMax");

								writer.WriteAttributeString("version", record.Face_BodyPropertiesMax_version);
								writer.WriteAttributeString("key", record.Face_BodyPropertiesMax_key);
								if (!record.Face_BodyPropertiesMax_age.Equals("")) writer.WriteAttributeString("age", record.Face_BodyPropertiesMax_age);
								if (!record.Face_BodyPropertiesMax_weight.Equals("")) writer.WriteAttributeString("weight", record.Face_BodyPropertiesMax_weight);
								if (!record.Face_BodyPropertiesMax_build.Equals("")) writer.WriteAttributeString("build", record.Face_BodyPropertiesMax_build);

								writer.WriteEndElement();
							}
						}

						if (!record.Face_hair_tags_hair_tag0_name.Equals("") || !record.Face_hair_tags_hair_tag1_name.Equals("") || !record.Face_hair_tags_hair_tag2_name.Equals("")) {
							writer.WriteStartElement("hair_tags");

							if (!record.Face_hair_tags_hair_tag0_name.Equals("")) {
								writer.WriteStartElement("hair_tag");
								writer.WriteAttributeString("name", record.Face_hair_tags_hair_tag0_name);
								writer.WriteEndElement();
							}
							if (!record.Face_hair_tags_hair_tag1_name.Equals("")) {
								writer.WriteStartElement("hair_tag");
								writer.WriteAttributeString("name", record.Face_hair_tags_hair_tag1_name);
								writer.WriteEndElement();
							}
							if (!record.Face_hair_tags_hair_tag2_name.Equals("")) {
								writer.WriteStartElement("hair_tag");
								writer.WriteAttributeString("name", record.Face_hair_tags_hair_tag2_name);
								writer.WriteEndElement();
							}

							writer.WriteEndElement();
						}
						if (!record.Face_beard_tags_beard_tag0_name.Equals("") || !record.Face_beard_tags_beard_tag1_name.Equals("") || !record.Face_beard_tags_beard_tag2_name.Equals("")) {
							writer.WriteStartElement("beard_tags");

							if (!record.Face_beard_tags_beard_tag0_name.Equals("")) {
								writer.WriteStartElement("beard_tag");
								writer.WriteAttributeString("name", record.Face_beard_tags_beard_tag0_name);
								writer.WriteEndElement();
							}
							if (!record.Face_beard_tags_beard_tag1_name.Equals("")) {
								writer.WriteStartElement("beard_tag");
								writer.WriteAttributeString("name", record.Face_beard_tags_beard_tag1_name);
								writer.WriteEndElement();
							}
							if (!record.Face_beard_tags_beard_tag2_name.Equals("")) {
								writer.WriteStartElement("beard_tag");
								writer.WriteAttributeString("name", record.Face_beard_tags_beard_tag2_name);
								writer.WriteEndElement();
							}

							writer.WriteEndElement();
						}
						if (!record.Face_tattoo_tags_tattoo_tag0_name.Equals("") || !record.Face_tattoo_tags_tattoo_tag1_name.Equals("") || !record.Face_tattoo_tags_tattoo_tag2_name.Equals("")) {
							writer.WriteStartElement("tattoo_tags");

							if (!record.Face_tattoo_tags_tattoo_tag0_name.Equals("")) {
								writer.WriteStartElement("tattoo_tag");
								writer.WriteAttributeString("name", record.Face_tattoo_tags_tattoo_tag0_name);
								writer.WriteEndElement();
							}
							if (!record.Face_tattoo_tags_tattoo_tag1_name.Equals("")) {
								writer.WriteStartElement("tattoo_tag");
								writer.WriteAttributeString("name", record.Face_tattoo_tags_tattoo_tag1_name);
								writer.WriteEndElement();
							}
							if (!record.Face_tattoo_tags_tattoo_tag2_name.Equals("")) {
								writer.WriteStartElement("tattoo_tag");
								writer.WriteAttributeString("name", record.Face_tattoo_tags_tattoo_tag2_name);
								writer.WriteEndElement();
							}

							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					//Companion
					if (record.is_companion.Equals("true", StringComparison.OrdinalIgnoreCase)) {
						writer.WriteStartElement("Components");
						writer.WriteStartElement("Companion");
						writer.WriteAttributeString(null, "id", null, record.id.Replace("companion_", "companion_comp_"));
						writer.WriteEndElement();
						writer.WriteEndElement();
					}

					if (!record.skills_skill0.Equals("")) {
						writer.WriteStartElement("skills");

						if (!record.skills_skill0.Equals("")) {
							writer.WriteStartElement("skill");
							writer.WriteAttributeString("id", record.skills_skill0.Split(";")[0]);
							writer.WriteAttributeString("value", record.skills_skill0.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.skills_skill1.Equals("")) {
							writer.WriteStartElement("skill");
							writer.WriteAttributeString("id", record.skills_skill1.Split(";")[0]);
							writer.WriteAttributeString("value", record.skills_skill1.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.skills_skill2.Equals("")) {
							writer.WriteStartElement("skill");
							writer.WriteAttributeString("id", record.skills_skill2.Split(";")[0]);
							writer.WriteAttributeString("value", record.skills_skill2.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.skills_skill3.Equals("")) {
							writer.WriteStartElement("skill");
							writer.WriteAttributeString("id", record.skills_skill3.Split(";")[0]);
							writer.WriteAttributeString("value", record.skills_skill3.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.skills_skill4.Equals("")) {
							writer.WriteStartElement("skill");
							writer.WriteAttributeString("id", record.skills_skill4.Split(";")[0]);
							writer.WriteAttributeString("value", record.skills_skill4.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.skills_skill5.Equals("")) {
							writer.WriteStartElement("skill");
							writer.WriteAttributeString("id", record.skills_skill5.Split(";")[0]);
							writer.WriteAttributeString("value", record.skills_skill5.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.skills_skill6.Equals("")) {
							writer.WriteStartElement("skill");
							writer.WriteAttributeString("id", record.skills_skill6.Split(";")[0]);
							writer.WriteAttributeString("value", record.skills_skill6.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.skills_skill7.Equals("")) {
							writer.WriteStartElement("skill");
							writer.WriteAttributeString("id", record.skills_skill7.Split(";")[0]);
							writer.WriteAttributeString("value", record.skills_skill7.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.skills_skill8.Equals("")) {
							writer.WriteStartElement("skill");
							writer.WriteAttributeString("id", record.skills_skill8.Split(";")[0]);
							writer.WriteAttributeString("value", record.skills_skill8.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.skills_skill9.Equals("")) {
							writer.WriteStartElement("skill");
							writer.WriteAttributeString("id", record.skills_skill9.Split(";")[0]);
							writer.WriteAttributeString("value", record.skills_skill9.Split(";")[1]);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					if (!record.Traits_trait0.Equals("")) {
						writer.WriteStartElement("Traits");

						if (!record.Traits_trait0.Equals("")) {
							writer.WriteStartElement("Trait");
							writer.WriteAttributeString("id", record.Traits_trait0.Split(";")[0]);
							writer.WriteAttributeString("value", record.Traits_trait0.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.Traits_trait1.Equals("")) {
							writer.WriteStartElement("Trait");
							writer.WriteAttributeString("id", record.Traits_trait1.Split(";")[0]);
							writer.WriteAttributeString("value", record.Traits_trait1.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.Traits_trait2.Equals("")) {
							writer.WriteStartElement("Trait");
							writer.WriteAttributeString("id", record.Traits_trait2.Split(";")[0]);
							writer.WriteAttributeString("value", record.Traits_trait2.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.Traits_trait3.Equals("")) {
							writer.WriteStartElement("Trait");
							writer.WriteAttributeString("id", record.Traits_trait3.Split(";")[0]);
							writer.WriteAttributeString("value", record.Traits_trait3.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.Traits_trait4.Equals("")) {
							writer.WriteStartElement("Trait");
							writer.WriteAttributeString("id", record.Traits_trait4.Split(";")[0]);
							writer.WriteAttributeString("value", record.Traits_trait4.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.Traits_trait5.Equals("")) {
							writer.WriteStartElement("Trait");
							writer.WriteAttributeString("id", record.Traits_trait5.Split(";")[0]);
							writer.WriteAttributeString("value", record.Traits_trait5.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.Traits_trait6.Equals("")) {
							writer.WriteStartElement("Trait");
							writer.WriteAttributeString("id", record.Traits_trait6.Split(";")[0]);
							writer.WriteAttributeString("value", record.Traits_trait6.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.Traits_trait7.Equals("")) {
							writer.WriteStartElement("Trait");
							writer.WriteAttributeString("id", record.Traits_trait7.Split(";")[0]);
							writer.WriteAttributeString("value", record.Traits_trait7.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.Traits_trait8.Equals("")) {
							writer.WriteStartElement("Trait");
							writer.WriteAttributeString("id", record.Traits_trait8.Split(";")[0]);
							writer.WriteAttributeString("value", record.Traits_trait8.Split(";")[1]);
							writer.WriteEndElement();
						}
						if (!record.Traits_trait9.Equals("")) {
							writer.WriteStartElement("Trait");
							writer.WriteAttributeString("id", record.Traits_trait9.Split(";")[0]);
							writer.WriteAttributeString("value", record.Traits_trait9.Split(";")[1]);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					//Equipment Set
					if (!record.equipmentSet0.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet0);
					if (!record.equipmentSet1.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet1);
					if (!record.equipmentSet2.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet2);
					if (!record.equipmentSet3.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet3);
					if (!record.equipmentSet4.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet4);
					if (!record.equipmentSet5.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet5);
					if (!record.equipmentSet6.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet6);
					if (!record.equipmentSet7.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet7);
					if (!record.equipmentSet8.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet8);
					if (!record.equipmentSet9.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet9);

					if (!record.equipmentSet10.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet10);
					if (!record.equipmentSet11.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet11);
					if (!record.equipmentSet12.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet12);
					if (!record.equipmentSet13.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet13);
					if (!record.equipmentSet14.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet14);
					if (!record.equipmentSet15.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet15);
					if (!record.equipmentSet16.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet16);
					if (!record.equipmentSet17.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet17);
					if (!record.equipmentSet18.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet18);
					if (!record.equipmentSet19.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet19);

					if (!record.equipmentSet20.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet20);
					if (!record.equipmentSet21.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet21);
					if (!record.equipmentSet22.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet22);
					if (!record.equipmentSet23.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet23);
					if (!record.equipmentSet24.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet24);
					if (!record.equipmentSet25.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet25);
					if (!record.equipmentSet26.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet26);
					if (!record.equipmentSet27.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet27);
					if (!record.equipmentSet28.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet28);
					if (!record.equipmentSet29.Equals("")) WriteEquipmentSetData(writer, record.equipmentSet29);

					if (!record.upgrade_targets_upgrade_target0_id.Equals("") || !record.upgrade_targets_upgrade_target1_id.Equals("") ||
						!record.upgrade_targets_upgrade_target2_id.Equals("") || !record.upgrade_targets_upgrade_target3_id.Equals("")) {
						writer.WriteStartElement("upgrade_targets");

						if (!record.upgrade_targets_upgrade_target0_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target0_id);
							writer.WriteEndElement();
						}
						if (!record.upgrade_targets_upgrade_target1_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target1_id);
							writer.WriteEndElement();
						}
						if (!record.upgrade_targets_upgrade_target2_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target2_id);
							writer.WriteEndElement();
						}
						if (!record.upgrade_targets_upgrade_target3_id.Equals("")) {
							writer.WriteStartElement("upgrade_target");
							writer.WriteAttributeString(null, "id", null, "NPCCharacter." + record.upgrade_targets_upgrade_target3_id);
							writer.WriteEndElement();
						}

						writer.WriteEndElement();
					}

					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
		} //Unfinished

		public static void NPCCharacters_XMLtoCSV(string xmlInput, string csvOutput) {
			List<NPCCharacterRecord> records = new List<NPCCharacterRecord>();

			using (XmlReader xmlReader = XmlReader.Create(xmlInput)) {
				while (xmlReader.Read()) {
				StartLoop:
					if (xmlReader.NodeType != XmlNodeType.Element) continue;
					//Console.WriteLine(xmlReader.Name);

					if (xmlReader.Name.Equals("NPCCharacter")) {
						NPCCharacterRecord record = new NPCCharacterRecord();

						record.id = xmlReader.GetAttribute("id");
						record.default_group = xmlReader.GetAttribute("default_group");
						record.voice = xmlReader.GetAttribute("voice");

						record.is_hero = xmlReader.GetAttribute("is_hero");
						record.is_female = xmlReader.GetAttribute("is_female");
						record.is_basic_troop = xmlReader.GetAttribute("is_basic_troop");
						record.is_template = xmlReader.GetAttribute("is_template");
						record.is_child_template = xmlReader.GetAttribute("is_child_template");

						record.culture = TrimD(xmlReader.GetAttribute("culture"));
						record.name = TrimB(xmlReader.GetAttribute("name"));
						record.banner_symbol_mesh_name = xmlReader.GetAttribute("banner_symbol_mesh_name");
						record.banner_symbol_color = xmlReader.GetAttribute("banner_symbol_color");
						record.banner_key = xmlReader.GetAttribute("banner_key");
						record.occupation = xmlReader.GetAttribute("occupation");
						record.civilianTemplate = TrimD(xmlReader.GetAttribute("civilianTemplate"));
						record.battleTemplate = TrimD(xmlReader.GetAttribute("battleTemplate"));

						record.is_companion = xmlReader.GetAttribute("is_companion");

						//TODO ensure offset is handled correctly (i.e. should it be trimmed?)
						record.offset = xmlReader.GetAttribute("offset");
						record.level = xmlReader.GetAttribute("level");
						record.age = xmlReader.GetAttribute("age");

						record.is_mercenary = xmlReader.GetAttribute("is_mercenary");

						record.formation_position_preference = xmlReader.GetAttribute("formation_position_preference");
						record.default_equipment_set = TrimD(xmlReader.GetAttribute("default_equipment_set"));
						record.skill_template = TrimD(xmlReader.GetAttribute("skill_template"));
						record.upgrade_requires = TrimD(xmlReader.GetAttribute("upgrade_requires"));

						int equipmentSet_counter = 0;

						while (xmlReader.Read()) {
						NextIteration:;
							if (xmlReader.NodeType != XmlNodeType.Element) continue;
							//Console.WriteLine("\t"+xmlReader.Name);

							switch (xmlReader.Name) {
								case "face":
									while (xmlReader.Read()) {
										if (xmlReader.NodeType != XmlNodeType.Element) continue;
										//Console.WriteLine("\t\t" + xmlReader.Name);

										switch (xmlReader.Name) {
											case "face_key":
												record.Face_face_key_value = xmlReader.GetAttribute("value");
												record.Face_face_key_max_value = xmlReader.GetAttribute("max_value");
												break;
											case "BodyProperties":
												record.Face_BodyProperties_version = xmlReader.GetAttribute("version");
												record.Face_BodyProperties_age = xmlReader.GetAttribute("age");
												record.Face_BodyProperties_weight = xmlReader.GetAttribute("weight");
												record.Face_BodyProperties_build = xmlReader.GetAttribute("build");
												record.Face_BodyProperties_key = xmlReader.GetAttribute("key");
												break;
											case "BodyPropertiesMax":
												record.Face_BodyPropertiesMax_version = xmlReader.GetAttribute("version");
												record.Face_BodyPropertiesMax_age = xmlReader.GetAttribute("age");
												record.Face_BodyPropertiesMax_weight = xmlReader.GetAttribute("weight");
												record.Face_BodyPropertiesMax_build = xmlReader.GetAttribute("build");
												record.Face_BodyPropertiesMax_key = xmlReader.GetAttribute("key");
												break;
											case "face_key_template":
												record.Face_face_key_template_value = TrimD(xmlReader.GetAttribute("value"));
												break;
											case "hair_tags":
												int hair_tags_counter = 0;
												while (xmlReader.Read() && !(xmlReader.NodeType.Equals(XmlNodeType.EndElement) && xmlReader.Name.Equals("hair_tags"))) {
													if (xmlReader.NodeType != XmlNodeType.Element) continue;

													if (xmlReader.Name.Equals("hair_tag")) {
														switch (hair_tags_counter) {
															case 0:
																record.Face_hair_tags_hair_tag0_name = xmlReader.GetAttribute("name");
																break;
															case 1:
																record.Face_hair_tags_hair_tag1_name = xmlReader.GetAttribute("name");
																break;
															case 2:
																record.Face_hair_tags_hair_tag2_name = xmlReader.GetAttribute("name");
																break;
														}
														hair_tags_counter++;
													} else Console.WriteLine("You... Shouldn't be here.");
												}
												break;
											case "beard_tags":
												int beard_tags_counter = 0;
												while (xmlReader.Read() && !(xmlReader.NodeType.Equals(XmlNodeType.EndElement) && xmlReader.Name.Equals("beard_tags"))) {
													if (xmlReader.NodeType != XmlNodeType.Element) continue;

													if (xmlReader.Name.Equals("beard_tag")) {
														switch (beard_tags_counter) {
															case 0:
																record.Face_beard_tags_beard_tag0_name = xmlReader.GetAttribute("name");
																break;
															case 1:
																record.Face_beard_tags_beard_tag1_name = xmlReader.GetAttribute("name");
																break;
															case 2:
																record.Face_beard_tags_beard_tag2_name = xmlReader.GetAttribute("name");
																break;
														}
														beard_tags_counter++;
													} else Console.WriteLine("You... Shouldn't be here.");
												}
												break;
											case "tattoo_tags":
												int tattoo_tags_counter = 0;
												while (xmlReader.Read() && !(xmlReader.NodeType.Equals(XmlNodeType.EndElement) && xmlReader.Name.Equals("tattoo_tags"))) {
													if (xmlReader.NodeType != XmlNodeType.Element) continue;

													if (xmlReader.Name.Equals("hair_tag")) {
														switch (tattoo_tags_counter) {
															case 0:
																record.Face_tattoo_tags_tattoo_tag0_name = xmlReader.GetAttribute("name");
																break;
															case 1:
																record.Face_tattoo_tags_tattoo_tag1_name = xmlReader.GetAttribute("name");
																break;
															case 2:
																record.Face_tattoo_tags_tattoo_tag2_name = xmlReader.GetAttribute("name");
																break;
														}
														tattoo_tags_counter++;
													} else Console.WriteLine("You... Shouldn't be here.");
												}
												break;
											default:
												goto NextIteration;
										}
									}
									break;
								case "Hero": // No vanilla NPCCharacters use this anymore - think it's for if you don't want to add 'em to heroes.xml, don't support this, just have that be used instead.
									xmlReader.Skip();
									break;
								case "Components": // Also doesn't seem to be used - implement eventually, though
									xmlReader.Skip();
									break;
								case "skills":
									int skills_counter = 0;
									while (xmlReader.Read()) {
										if (xmlReader.NodeType != XmlNodeType.Element) continue;
										//Console.WriteLine("\t\t" + xmlReader.Name);

										switch (xmlReader.Name) {
											case "skill":
												switch (skills_counter) {
													case 0:
														record.skills_skill0 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 1:
														record.skills_skill1 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 2:
														record.skills_skill2 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 3:
														record.skills_skill3 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 4:
														record.skills_skill4 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 5:
														record.skills_skill5 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 6:
														record.skills_skill6 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 7:
														record.skills_skill7 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 8:
														record.skills_skill8 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 9:
														record.skills_skill9 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
												}

												skills_counter++;
												break;
											default:
												goto NextIteration;
										}
									}
									break;
								case "Traits":
									int Traits_counter = 0;
									while (xmlReader.Read()) {
										if (xmlReader.NodeType != XmlNodeType.Element) continue;
										//Console.WriteLine("\t\t" + xmlReader.Name);

										switch (xmlReader.Name) {
											case "Trait":
												switch (Traits_counter) {
													case 0:
														record.Traits_trait0 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 1:
														record.Traits_trait1 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 2:
														record.Traits_trait2 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 3:
														record.Traits_trait3 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 4:
														record.Traits_trait4 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 5:
														record.Traits_trait5 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 6:
														record.Traits_trait6 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 7:
														record.Traits_trait7 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 8:
														record.Traits_trait8 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
													case 9:
														record.Traits_trait9 = xmlReader.GetAttribute("id") + ";" + xmlReader.GetAttribute("value");
														break;
												}

												Traits_counter++;
												break;
											default:
												goto NextIteration;
										}
									}
									break;
								case "feats": // And again, doesn't seem to be used - implement eventually, though.
									xmlReader.Skip();
									break;
								case "equipmentSet":
									string equipmentSet_result = "";

									if (xmlReader.GetAttribute("civilian") is null || xmlReader.GetAttribute("civilian").Equals("false")) equipmentSet_result += "false";
									else equipmentSet_result += "true";

									while (xmlReader.Read()) {
										if (xmlReader.NodeType != XmlNodeType.Element) continue;
										//Console.WriteLine("\t\t" + xmlReader.Name);

										switch (xmlReader.Name) {
											case "equipment":
												equipmentSet_result += ";" + xmlReader.GetAttribute("slot") + ";" + xmlReader.GetAttribute("id");
												if (xmlReader.GetAttribute("amount") is null) equipmentSet_result += ";-1";
												else equipmentSet_result += ";" + xmlReader.GetAttribute("amount");
												break;
											default:
												goto equipmentSet_end;
										}
									}
								equipmentSet_end:
									switch (equipmentSet_counter) {
										case 0:
											record.equipmentSet0 = equipmentSet_result;
											break;
										case 1:
											record.equipmentSet1 = equipmentSet_result;
											break;
										case 2:
											record.equipmentSet2 = equipmentSet_result;
											break;
										case 3:
											record.equipmentSet3 = equipmentSet_result;
											break;
										case 4:
											record.equipmentSet4 = equipmentSet_result;
											break;
										case 5:
											record.equipmentSet5 = equipmentSet_result;
											break;
										case 6:
											record.equipmentSet6 = equipmentSet_result;
											break;
										case 7:
											record.equipmentSet7 = equipmentSet_result;
											break;
										case 8:
											record.equipmentSet8 = equipmentSet_result;
											break;
										case 9:
											record.equipmentSet9 = equipmentSet_result;
											break;
										case 10:
											record.equipmentSet10 = equipmentSet_result;
											break;
										case 11:
											record.equipmentSet11 = equipmentSet_result;
											break;
										case 12:
											record.equipmentSet12 = equipmentSet_result;
											break;
										case 13:
											record.equipmentSet13 = equipmentSet_result;
											break;
										case 14:
											record.equipmentSet14 = equipmentSet_result;
											break;
										case 15:
											record.equipmentSet15 = equipmentSet_result;
											break;
										case 16:
											record.equipmentSet16 = equipmentSet_result;
											break;
										case 17:
											record.equipmentSet17 = equipmentSet_result;
											break;
										case 18:
											record.equipmentSet18 = equipmentSet_result;
											break;
										case 19:
											record.equipmentSet19 = equipmentSet_result;
											break;
										case 20:
											record.equipmentSet20 = equipmentSet_result;
											break;
										case 21:
											record.equipmentSet21 = equipmentSet_result;
											break;
										case 22:
											record.equipmentSet22 = equipmentSet_result;
											break;
										case 23:
											record.equipmentSet23 = equipmentSet_result;
											break;
										case 24:
											record.equipmentSet24 = equipmentSet_result;
											break;
										case 25:
											record.equipmentSet25 = equipmentSet_result;
											break;
										case 26:
											record.equipmentSet26 = equipmentSet_result;
											break;
										case 27:
											record.equipmentSet27 = equipmentSet_result;
											break;
										case 28:
											record.equipmentSet28 = equipmentSet_result;
											break;
										case 29:
											record.equipmentSet29 = equipmentSet_result;
											break;
										default:
											if (equipmentSet_counter == AllowedEquipmentSets) AffectedNPCCharacters++;
											if (NeededEquipmentSets < equipmentSet_counter) NeededEquipmentSets = equipmentSet_counter;
											break;
									}
									equipmentSet_counter++;
									goto NextIteration;
								case "equipment": //TODO probably implement it as raw data, rather than something for a csv, though... Or perhaps create a format for it in the csv?
									xmlReader.Skip();
									break;
								case "upgrade_targets":
									int upgrade_targets_counter = 0;
									while (xmlReader.Read()) {
										if (xmlReader.NodeType != XmlNodeType.Element) continue;
										//Console.WriteLine("\t\t" + xmlReader.Name);

										switch (xmlReader.Name) {
											case "upgrade_target":
												switch (upgrade_targets_counter) {
													case 0:
														record.upgrade_targets_upgrade_target0_id = TrimD(xmlReader.GetAttribute("id"));
														break;
													case 1:
														record.upgrade_targets_upgrade_target1_id = TrimD(xmlReader.GetAttribute("id"));
														break;
													case 2:
														record.upgrade_targets_upgrade_target2_id = TrimD(xmlReader.GetAttribute("id"));
														break;
													case 3:
														record.upgrade_targets_upgrade_target3_id = TrimD(xmlReader.GetAttribute("id"));
														break;
												}

												upgrade_targets_counter++;
												break;
											default:
												goto NextIteration;
										}
									}
									break;
								default:
									records.Add(record);
									goto StartLoop;
							}
						}

						if (!records.Contains(record)) records.Add(record);
					}
				}
			}

			using (CsvWriter csvWriter = new CsvWriter(new StreamWriter(csvOutput), CultureInfo.InvariantCulture)) {
				csvWriter.WriteRecords(records);
				csvWriter.Flush();
			}
		}

		public class NPCCharacterRecord {
			public string id { get; set; }
			public string name { get; set; }
			public string level { get; set; }
			public string age { get; set; }
			public string default_group { get; set; }
			public string voice { get; set; }
			public string is_hero { get; set; }
			public string is_female { get; set; }
			public string is_basic_troop { get; set; }
			public string is_template { get; set; }
			public string is_child_template { get; set; }
			public string culture { get; set; }
			public string banner_symbol_mesh_name { get; set; }
			public string banner_symbol_color { get; set; }
			public string banner_key { get; set; }
			public string occupation { get; set; }
			public string civilianTemplate { get; set; }
			public string battleTemplate { get; set; }
			public string is_companion { get; set; }
			public string offset { get; set; }
			public string is_mercenary { get; set; }
			public string formation_position_preference { get; set; }
			public string default_equipment_set { get; set; }
			public string skill_template { get; set; }
			public string upgrade_requires { get; set; }

			public string upgrade_targets_upgrade_target0_id { get; set; }
			public string upgrade_targets_upgrade_target1_id { get; set; }
			public string upgrade_targets_upgrade_target2_id { get; set; }
			public string upgrade_targets_upgrade_target3_id { get; set; }

			//Face - face_key
			public string Face_face_key_value { get; set; }
			public string Face_face_key_max_value { get; set; }
			public string Face_face_key_template_value { get; set; }
			//Face - BodyProperties
			public string Face_BodyProperties_version { get; set; }
			public string Face_BodyProperties_key { get; set; }
			public string Face_BodyProperties_age { get; set; }
			public string Face_BodyProperties_weight { get; set; }
			public string Face_BodyProperties_build { get; set; }
			//Face - BodyPropertiesMax
			public string Face_BodyPropertiesMax_version { get; set; }
			public string Face_BodyPropertiesMax_key { get; set; }
			public string Face_BodyPropertiesMax_age { get; set; }
			public string Face_BodyPropertiesMax_weight { get; set; }
			public string Face_BodyPropertiesMax_build { get; set; }
			//Face - hair_tags
			public string Face_hair_tags_hair_tag0_name { get; set; }
			public string Face_hair_tags_hair_tag1_name { get; set; }
			public string Face_hair_tags_hair_tag2_name { get; set; }
			//Face - beard_tags
			public string Face_beard_tags_beard_tag0_name { get; set; }
			public string Face_beard_tags_beard_tag1_name { get; set; }
			public string Face_beard_tags_beard_tag2_name { get; set; }
			//Face - tattoo_tags
			public string Face_tattoo_tags_tattoo_tag0_name { get; set; }
			public string Face_tattoo_tags_tattoo_tag1_name { get; set; }
			public string Face_tattoo_tags_tattoo_tag2_name { get; set; }

			//skills
			public string skills_skill0 { get; set; }
			public string skills_skill1 { get; set; }
			public string skills_skill2 { get; set; }
			public string skills_skill3 { get; set; }
			public string skills_skill4 { get; set; }
			public string skills_skill5 { get; set; }
			public string skills_skill6 { get; set; }
			public string skills_skill7 { get; set; }
			public string skills_skill8 { get; set; }
			public string skills_skill9 { get; set; }

			//Traits
			public string Traits_trait0 { get; set; }
			public string Traits_trait1 { get; set; }
			public string Traits_trait2 { get; set; }
			public string Traits_trait3 { get; set; }
			public string Traits_trait4 { get; set; }
			public string Traits_trait5 { get; set; }
			public string Traits_trait6 { get; set; }
			public string Traits_trait7 { get; set; }
			public string Traits_trait8 { get; set; }
			public string Traits_trait9 { get; set; }

			//equipmentSet
			public string equipmentSet0 { get; set; }
			public string equipmentSet1 { get; set; }
			public string equipmentSet2 { get; set; }
			public string equipmentSet3 { get; set; }
			public string equipmentSet4 { get; set; }
			public string equipmentSet5 { get; set; }
			public string equipmentSet6 { get; set; }
			public string equipmentSet7 { get; set; }
			public string equipmentSet8 { get; set; }
			public string equipmentSet9 { get; set; }

			public string equipmentSet10 { get; set; }
			public string equipmentSet11 { get; set; }
			public string equipmentSet12 { get; set; }
			public string equipmentSet13 { get; set; }
			public string equipmentSet14 { get; set; }
			public string equipmentSet15 { get; set; }
			public string equipmentSet16 { get; set; }
			public string equipmentSet17 { get; set; }
			public string equipmentSet18 { get; set; }
			public string equipmentSet19 { get; set; }

			public string equipmentSet20 { get; set; }
			public string equipmentSet21 { get; set; }
			public string equipmentSet22 { get; set; }
			public string equipmentSet23 { get; set; }
			public string equipmentSet24 { get; set; }
			public string equipmentSet25 { get; set; }
			public string equipmentSet26 { get; set; }
			public string equipmentSet27 { get; set; }
			public string equipmentSet28 { get; set; }
			public string equipmentSet29 { get; set; }

		}
		public static void WriteEquipmentSetData(XmlWriter writer, string data) {
			writer.WriteStartElement("equipmentSet");
			string[] eqData = data.Split(";");

			if ("true".Equals(eqData[0])) writer.WriteAttributeString("civilian", "true");

			for (int i = 1; i < eqData.Length; i++) {
				switch (i % 3) {
					case 1:
						writer.WriteStartElement("equipment");
						writer.WriteAttributeString("slot", eqData[i]);
						break;
					case 2:
						writer.WriteAttributeString("id", eqData[i]);
						break;
					case 0:
						if (!eqData[i].Equals("-1")) writer.WriteAttributeString("amount", eqData[i]);
						writer.WriteEndElement();
						break;
				}
			}

			writer.WriteEndElement();
		}
	}
}
