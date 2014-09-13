using System;
using System.Collections.Generic;
using System.Text;

namespace NetFocus.UtilityTool.CodeGenerator.Commands.Components
{
    public enum MatchKey
    {
        ApplicationName,
        ApplicationLowerName,
        MasterPageName,
        MPContentID,
        EntityTypeName,
        EntityTypeFirstNameLower,
        EntityTypeChineseName,
        EntityTypeLowerName,
        ManageControlName,
        PrefixString,
        PrefixLowerString,
        RoleNames,
        ManageControlTitleFieldName,
        PageSize,
        EntityTypeEnum,
        EntityFieldResourceList,
        FieldControlHTMLList,
        EntityFieldExtendedAttributeList,
        CSListMatch_ASCXCS_EntityFieldGetValueList,
        CSListMatch_ASCXCS_EntityFieldGetControlList,
        CSListMatch_ASCXCS_EntityFieldDefineList,
        CSListMatch_ASCXCS_EntityFieldAssignValueList,
    }

    public enum NWAPMatchKey
    {
        EntityTypeName,
        ListMatch_ASCXCS_ExtendedFieldDefineList,
    }
}
