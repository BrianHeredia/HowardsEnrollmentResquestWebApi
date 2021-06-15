using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Howards.Models
{
    [Table("Request")]
    public class Request
    {
        /// <example>2b356bae-c7f5-4730-90b8-50be9f65bcfb</example>
        [Key]
        [Required]
        public Guid RequestId { get; set; }

        /// <example>Brian</example>
        [StringLength(20)]
        [RegularExpression("(^[a-zA-Z]+$)", ErrorMessage = "The field FirstName should be only letters")]
        [Required]
        public string FirstName { get; set; }

        /// <example>Heredia</example>
        [StringLength(20)]
        [RegularExpression("(^[a-zA-Z]+$)", ErrorMessage = "The field FirstName should be only letters")]
        [Required]
        public string LastName { get; set; }

        /// <example>1234567890</example>
        [StringLength(10)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "The field FirstName should be only numbers")]
        [Required]
        public string Id { get; set; }

        /// <example>25</example>
        [StringLength(2)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "The field FirstName should be only numbers")]
        [Required]
        public string Age { get; set; }

        /// <example>Hufflepuff</example>
        [Required]
        [EnumDataType(typeof(House), ErrorMessage = "The field House should only be: 0 for Gryffindor, 1 for Hufflepuff, 2 for Ravenclaw and 3 for Slytherin")]
        [JsonConverter(typeof(StringEnumConverter))]
        public House House { get; set; }
    }

    public enum House
    {
        /// <summary>
        /// Gryffindor
        /// </summary>
        Gryffindor,
        /// <summary>
        /// Hufflepuff
        /// </summary>
        Hufflepuff,
        /// <summary>
        /// Ravenclaw
        /// </summary>
        Ravenclaw,
        /// <summary>
        /// Slytherin
        /// </summary>
        Slytherin
    }
}
