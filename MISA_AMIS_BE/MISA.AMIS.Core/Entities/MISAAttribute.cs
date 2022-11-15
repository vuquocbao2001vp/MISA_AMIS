using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    #region Attribute

    /// <summary>
    /// Trường thông tin bắt buộc
    /// </summary>
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field)]
    public class Required: Attribute
    {

    }

    /// <summary>
    /// Các trường thông tin không được trùng
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Duplicated: Attribute
    {

    }

    /// <summary>
    /// Định dạng email
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Email : Attribute
    {

    }

    /// <summary>
    /// Tên attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PropNameDisplay : Attribute
    {
        public string PropName { get; set; }
        public PropNameDisplay(string name)
        {
            this.PropName = name;
        }
    }

    #endregion
}
