using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HC.Core
{
    public static class Enums
    {
        public static List<SelectListItem> GetEnumList<T>()
        {
            var itemValues = Enum.GetValues(typeof(T));
            var itemNames = Enum.GetNames(typeof(T));

            List<SelectListItem> lstDR = new List<SelectListItem>();
            foreach (var itm in itemValues)
            {
                Enum en = (Enum)Enum.Parse(typeof(T), itm.ToString(), true);

                SelectListItem dr = new SelectListItem();
                dr.Text = en.GetDescription();
                dr.Value = en.ToString();

                lstDR.Add(dr);
            }
            return lstDR;
        }

        public static string GetDesc(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        public static List<SelectListItem> GetEnumListDesc<T>()
        {
            var itemValues = Enum.GetValues(typeof(T));
            var itemNames = Enum.GetNames(typeof(T));

            List<SelectListItem> lstDR = new List<SelectListItem>();
            foreach (var itm in itemValues)
            {
                Enum en = (Enum)Enum.Parse(typeof(T), itm.ToString(), true);

                SelectListItem dr = new SelectListItem();
                dr.Text = en.GetDesc();
                dr.Value = Convert.ToInt32(en).ToString();

                lstDR.Add(dr);
            }
            return lstDR;
        }
    }
    public enum AuthenticationType
    {
        Authenticated = 1,
        Anonymous = 2
    }

    public enum EmailFormat
    {
        [Display(Name = "SMTP (This sends email directly and requires the information below)")]
        SMTP = 1,

        [Display(Name = "MAPI (This sends email through your existing email program)")]
        MAPI1 = 2,

        [Display(Name = "MAPI (Create but don't send)")]
        MAPI2 = 3
    }

    public enum Gender
    {
        Unknown = 0,
        Male = 1,
        Female = 2,
    }

    public enum ObjectState
    {
        Added,
        Modified,
        Deleted
    }

    public enum UserRoles
    {
        SuperAdmin = 1, // data access not allowed or data accessible
        Agent = 6,
        Organisation = 5,
        Charity = 7,
        Branch = 8, // data access not allowed
        Donor = 9, // data access not allowed
        Internal = 10,
        TechnicalSupport = 4, // data access not allowed or data accessible
        Input = 91
    }

    public enum MessageType
    {
        Info,
        Warning,
        Danger,
        Success
    }

    public enum ModalSize
    {
        Small,
        Medium,
        Large,
        ExtraLarge
    }

    public enum CredentialsRequest
    {
        UserId = 1,
        UserPassword = 2
    }

    public enum BindViewBag
    {
        Organisation,
        Charity,
        Branch,
        MembershipType,
        GroupType,
        SkillGroup,
        SkillCharity,
        LinkBranch,
        MGOMMOCharity,
        MGOMMOBranch,
        Group
        ////Letter,
        ////CharityBranches,
        ////Users,
        ////Labels,
        ////Donors
    }





    public enum UserPreferenceOrderByCharityBranch
    {
        [Display(Name = "By Name")]
        ByName = 1,
        [Display(Name = "By Reference")]
        ByRef = 2
    }

    #region "All Table Names"
    public enum DataEnityNames
    {
        CentralOffice,
        Charity,
        Branch,
        User,
        Menu,
        MMOMembershipType,
        MMOMembershipCode,
        MMOContactType,
        MMORelationshipType,
        MMOUserDefinedField,
        Person,
        HouseHold,
        Address,
        MMOContact,
        MMORelationshipMember,
        MMOCorrespondence,
        MMOUserDefined,
        MMOMembership,
        MMOGroupType,
        MMOGroup,
        MMOGroup1,
        MMOGroupMembers,
        MMOGroupEvent,
        MMOGroupEventAttendance,
        MMOSkillGroup,
        MMOSkill,
        MMOCertificate,
        MMOCertificateIssuer,
        MMOMemberSkill,
        MMOMemberCertificate,
        MMOMasterTask,
        MMOTaskSkill,
        MMOTaskWillingMember,
        MMOMemberRule,
        MMOTaskShift,
        MMOVisitType,
        MMOVisit,
        Role,
        MMOReportLabel,
        MMOAttendanceCode,
        MMOMaritalStatus,
        Donor,
        MMONotes,
        MMOMembershipEnrolmentForm
        //Client,
        //DonorType,
        //Menu,
        //User,
        //Donor,
        //Method,
        //ContactType,
        //StandardComments,
        //Purpose,
        //ConnectedCharity,
        //Country,
        //Envelope,
        //TaxYear,
        //Letter,
        //Charity,
        //Branch,
        //Agent,
        //SmartFilter,
        //QuickDonorGift,
        //Donation,
        //RegularGift,
        //Batch,
        //StandingGift,
        //BankStatementTemplate,
        //Label,
        //MMOMembershipType,
        //MMOContactType,
        //MMOMembershipCode,
        //MMORelationshipType,
        //MMOUserDefinedField
    }
    #endregion

    public enum UserFieldType
    {
        Text = 1,
        Numeric = 2,
        Date = 3,
        List = 4,
        Logical = 5
    }


    public enum CharityClaimType
    {
        [Display(Name = "Claim as an agent")]
        ClaimAsAgent = 1,

        [Display(Name = "Claim as this authorised official")]
        ClaimAsAuthorize = 2,

        [Display(Name = "Claim under a Central HMRC Reference")]
        ClaimAsHMRCReference = 3
    }

    public enum AddressTypes
    {
        Primary = 0,
        Alternative = 1,
        // Other = 2
    }

    public enum PersonTypes
    {
        Family = 0,
        Individual = 1
    }

    //public enum Marital
    //{
    //    Unknown = 0,
    //    Married = 1,
    //    Single = 2,
    //    Divorced = 3,
    //    Partnership = 4,
    //    Separated = 5,
    //    Widow = 6,
    //    Widower = 7,
    //}

    public enum ChangeFamilyType
    {
        [Display(Name = "Move this person to another existing family")]
        ExistingFamily = 0,
        [Display(Name = "Create a new family for this person")]
        NewFamily = 1,
    }

    public enum DefaultContactType
    {
        [Display(Name = "Email")]
        Email = 0,
        [Display(Name = "Home")]
        Home = 1,
        [Display(Name = "Mobile")]
        Mobile = 2,
        [Display(Name = "Office")]
        Office = 3,
        [Display(Name = "Fax")]
        Fax = 4,
        [Display(Name = "Website")]
        Website = 5
    }

    //public enum Attended
    //{
    //    Absent = 0,
    //    Attended = 1
    //}

    public enum AttendanceCodeFor
    {
        Both = 0,
        Group = 1,
        Rota = 2
    }

    public enum RenewalFrequency
    {
        [Display(Name = "Weekly")]
        Weekly = 1,
        [Display(Name = "Monthly")]
        Monthly = 2,
        [Display(Name = "BiMonthly")]
        BiMonthly = 3,
        Quarterly = 4,
        [Display(Name = "6-Monthly")]
        SixMonthly = 5,
        Yearly = 6,
        Custom = 7
    }

    public enum FeeFrequency
    {
        [Display(Name = "Weekly")]
        Weekly = 1,
        [Display(Name = "Monthly")]
        Monthly = 2,
        [Display(Name = "BiMonthly")]
        BiMonthly = 3,
        Quarterly = 4,
        [Display(Name = "6-Monthly")]
        SixMonthly = 5,
        Yearly = 6,
    }

    public enum FeeFrequencyCopy
    {
        [Display(Name = "Week")]
        Weekly = 1,
        [Display(Name = "Month")]
        Monthly = 2,
        [Display(Name = "Bi-Month")]
        BiMonthly = 3,
        Quarterly = 4,
        [Display(Name = "6-Month")]
        SixMonthly = 5,
        [Display(Name = "Year")]
        Yearly = 6,
    }

    public enum FeeStatus
    {
        [Display(Name = "OutStanding")]
        OutStanding = 1,
        [Display(Name = "OverDue")]
        OverDue = 2,
        [Display(Name = "Paid Out")]
        Paid = 3
    }

    public enum RulePreferenceType
    {
        [Display(Name = "Always wants to work with:")]
        AlwaysWantsToWorkWith = 1,
        [Display(Name = "Is unable to work with:")]
        IsUnableToWorkWith = 2,
        [Display(Name = "Would prefer to work with:")]
        WouldPreferToWorkWith = 3,
        [Display(Name = "Would prefer not to work with:")]
        WouldPreferNotToWorkWith = 4
    }

    public enum ScheduleFrequency
    {
        [Display(Name = "Daily Basis")]
        Daily = 1,
        [Display(Name = "Weekly Basis")]
        Weekly = 2,
        [Display(Name = "Monthly Basis")]
        Monthly = 3,
        [Display(Name = "Annual Basis")]
        Annual = 4,
    }

    public enum WeekDay
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    public enum MonthWeek
    {
        [Display(Name = "First")]
        First = 1,
        [Display(Name = "Second")]
        Second = 2,
        [Display(Name = "Third")]
        Third = 3,
        [Display(Name = "Fourth")]
        Fourth = 4,
        [Display(Name = "Last")]
        Last = 5
    }

    public enum Month
    {
        [Display(Name = "January")]
        January = 1, //January - 31 days   
        [Display(Name = "February")]
        February = 2, //February - 28 days in a common year and 29 days in leap years
        [Display(Name = "March")]
        March = 3, // March - 31 days 
        [Display(Name = "April")]
        April = 4, //April - 30 days       
        [Display(Name = "May")]
        May = 5, // May - 31 days
        [Display(Name = "June")]
        June = 6, //June - 30 days       
        [Display(Name = "July")]
        July = 7, //July - 31 days   
        [Display(Name = "August")]
        August = 8, //August - 31 days   
        [Display(Name = "September")]
        September = 9, //September - 30 days    
        [Display(Name = "October")]
        October = 10, //October - 31 days 
        [Display(Name = "November")]
        November = 11, //November - 30 days   
        [Display(Name = "December")]
        December = 12 // December - 31 days
    }

    public enum RotaAttended
    {
        [Display(Name = "Absent")]
        Absent = 0,
        [Display(Name = "Completed")]
        Completed = 1
    }

    #region "All SP Names"

    public enum DataSPNames
    {
        sp_MMOBranchClearData,
        sp_MMOCharityClearData,
        sp_MMOOrgClearData,
        sp_MMOMemberInActive,
        sp_MMOMemberReactivate,
        sp_MMOMemberDeceased,
        sp_MMODeleteMember,
        sp_MMOHouseHoldInActive,
        sp_MMOHouseHoldReactivate,
        sp_MMOMembershipCode,
        sp_MMORemoveMSTenant,
        sp_MMODeleteGroup,
        sp_MCCreateUsers,
        sp_MCRemoveUser
        //sp_DCCreateUsers,
        //sp_DonorClearData,
        //sp_TablesDeleteData,
        //sp_StandardComments,
        //sp_DCRemoveUser,
        //sp_MoveDonorBranch,
        //sp_MergeDonor
    }

    #endregion

    public enum EmailSendBy
    {
        SMTP = 0,
        MAPI = 1
    }

    public enum MemberListFilter
    {
        [Display(Name = "Everyone")]
        Everyone = 1,
        [Display(Name = "Members of the following Group")]
        MembersOfTheFollowingGroup = 2,
        [Display(Name = "Members of any Group of type")]
        MembersOfAnyGroupOfType = 3,
        [Display(Name = "Tagged Members")]
        TaggedMembers = 4,
    }

    public enum FamilyListFilter
    {
        [Display(Name = "Everyone")]
        Everyone = 1,
        [Display(Name = "Members of the following People Group")]
        MembersOfTheFollowingPeopleGroup = 2,
        [Display(Name = "Members of any Group of type")]
        MembersOfAnyGroupOfType = 3,
        [Display(Name = "Members of the following Family Group")]
        MembersOfTheFollowingFamilyGroup = 4,
    }

    public enum ReportStyle
    {
        [Display(Name = "Family List")]
        FamilyList = 1,
        [Display(Name = "Mobile Numbers and Emails")]
        MobileNumbersAndEmails = 2
    }

    public enum ShowNamesAs
    {
        [Display(Name = "Regular (John Smith)")]
        Regular = 1,
        [Display(Name = "File As  (Smith, John)")]
        FileAs = 2
    }

    public enum ReportType
    {
        [Display(Name = "SimpleListofPeople")]
        SimpleListofPeople,


        [Display(Name = "SimpleListofFamily")]
        SimpleListofFamily,


        [Display(Name = "SimpleListofPeopleLabel")]
        SimpleListofPeopleLabel,
        [Display(Name = "SimpleListofPeopleLabelList")]
        SimpleListofPeopleLabelList,


        [Display(Name = "SimpleListofFamilyLabel")]
        SimpleListofFamilyLabel,
        [Display(Name = "SimpleListofFamilyLabelList")]
        SimpleListofFamilyLabelList,

        [Display(Name = "PersonalRecord")]
        PersonalRecord,

        [Display(Name = "FamilyRecord")]
        FamilyRecord,


    }



    public enum ExportType
    {
        PDF = 1,
        Excel = 2,
        CSV = 3,
        Word = 4
    }

    public enum ImportType
    {
        [Display(Name = "Overwrite Data")]
        OverWrite,
        [Display(Name = "Append Data")]
        Append,
        [Display(Name = "Link Data")]
        Link
    }

    public enum ImportAs
    {
        [Display(Name = "Charity")]
        Charity,
        [Display(Name = "Branch")]
        Branch,
        [Display(Name = "Link Branch")]
        LinkBranch
    }

    #region "DBF File Enums"

    public enum DBFTableName
    {
        ADDRESS,
        CITY,
        CORRESP,
        COUNTRY,
        //GRAPHPROFILES,
        GROUP,
        GROUPATTEND,
        GROUPLINK,
        GROUPSCHEDULE,
        GROUPTYPE,
        GROUPTYPELINK,
        HOUSEHOLD,
        //LABREPF,
        //LABREPS,
        MARITALCODE,
        //MASTERMENU,
        //MCAUTO,
        //MCCATS,
        //MCGLINK,
        //MCGROUPS,
        //MCLISTS,
        //MCMEMBERS,
        MEMBERCODE,
        MEMBERLINK,
        ORGANIZATION,
        //ORGATTEND,
        //PARAMETERDDEV,
        PARAMETERORG,
        //PARAMETERUSER,
        PEOPLE,
        PHONE,
        PHONETYPE,
        PICTURES,
        POSTALCITY,
        REASON,
        REL,
        RELCODE,
        ROTA,
        ROTARULES,
        SECURITYCHECK,
        SECURITYCHECKCODE,
        //SGCRITERIA,
        //SMARTGROUP,
        //SRCLCC1,
        //SRCLCC2,
        //SRCLDGK,
        //SRCLELOG,
        //SRCLFCT1,
        //SRCLIL,
        //SRCLIM,
        //SRCLIS,
        //SRCLISTR,
        //SRCLSCG,
        //SRCLSCGD,
        //SRCLSEC,
        //SRCLSFG,
        //SRCLSMG,
        //SRCLSUSR,
        //SRCLUACT,
        //SRCLUAUD,
        TASK,
        TASKTYPELINK,
        //TEMP,
        TITLE,
        UFIELDDEF,
        UFIELDS,
        UNAVAIL,
        VISITLINK,
        VISITS,
        WILLDO,
    }


    //public enum DBFTableName
    //{
    //    ADDRESS,
    //    CITY,
    //    CORRESP,
    //    COUNTRY,
    //    GRAPHPROFILES,
    //    GROUP,
    //    GROUPATTEND,
    //    GROUPLINK,
    //    GROUPSCHEDULE,
    //    GROUPTYPE,
    //    GROUPTYPELINK,
    //    HOUSEHOLD,
    //    LABREPF,
    //    LABREPS,
    //    MARITALCODE,
    //    MASTERMENU,
    //    MCAUTO,
    //    MCCATS,
    //    MCGLINK,
    //    MCGROUPS,
    //    MCLISTS,
    //    MCMEMBERS,
    //    MEMBERCODE,
    //    MEMBERLINK,
    //    ORGANIZATION,
    //    ORGATTEND,
    //    PARAMETERDDEV,
    //    PARAMETERORG,
    //    PARAMETERUSER,
    //    PEOPLE,
    //    PHONE,
    //    PHONETYPE,
    //    PICTURES,
    //    POSTALCITY,
    //    REASON,
    //    REL,
    //    RELCODE,
    //    ROTA,
    //    ROTARULES,
    //    SECURITYCHECK,
    //    SECURITYCHECKCODE,
    //    SGCRITERIA,
    //    SMARTGROUP,
    //    SRCLCC1,
    //    SRCLCC2,
    //    SRCLDGK,
    //    SRCLELOG,
    //    SRCLFCT1,
    //    SRCLIL,
    //    SRCLIM,
    //    SRCLIS,
    //    SRCLISTR,
    //    SRCLSCG,
    //    SRCLSCGD,
    //    SRCLSEC,
    //    SRCLSFG,
    //    SRCLSMG,
    //    SRCLSUSR,
    //    SRCLUACT,
    //    SRCLUAUD,
    //    TASK,
    //    TASKTYPELINK,
    //    TEMP,
    //    TITLE,
    //    UFIELDDEF,
    //    UFIELDS,
    //    UNAVAIL,
    //    VISITLINK,
    //    VISITS,
    //    WILLDO,
    //}


    #endregion

    public enum ActivityType
    {
        Other = 1,
        RemoveDataFromMGO = 2,
        DonateNowFailed = 3
    }

    public enum ReferenceType
    {
        Numeric = 1,
        Character = 2
    }

    public enum IncludeTables
    {
        Person_MmopersonAdditonalDetails
    }

    public enum NotesPrivacy
    {
        Public = 0,
        Private = 1
    }

    public enum CommunicationPreference
    {
        Post = 0,
        Email = 1
    }

    public enum FeePayable
    {
        OneOff = 0,
        DirectDebit = 1
    }

    public enum OrganistionType
    {
        MyGivingOnline = 1,
        MyMembership = 2,
        MyGivingOnlineAndMyMembership = 3
    }

    public enum PaymentMethod
    {
        [Display(Name = "GoCardless")]
        GoCardLess = 2,
        [Display(Name = "Paypal")]
        Paypal = 3,
        [Display(Name = "Braintree")]
        Braintree = 5,
    }

    public enum ButtonType
    {
        DonateNow = 1,
        RegularDonateNow = 2,
        AddNewDonor = 3,
        PayACharityDonateNow = 4,
        TrueLayerDonateNow = 5,
    }
    public enum PaypalCurrency
    {
        GBP = 1, // Pound sterling
        AUD = 2, //Australian dollar   
        BRL = 3, //Brazilian real 	
        CAD = 4, // Canadian dollar 
        CZK = 5, //Czech koruna
        DKK = 6,  //Danish krone    
        EUR = 7, //Euro
        HKD = 8, //Hong Kong dollar    
        HUF = 9, //Hungarian forint 
        INR = 10, //Indian rupee
        ILS = 11, // Israeli new shekel 
        JPY = 12, // Japanese yen 
        MYR = 13, // Malaysian ringgit 
        MXN = 14, //Mexican peso
        TWD = 15, //New Taiwan dollar 
        NZD = 16, //  New Zealand dollar 
        NOK = 17, //  Norwegian krone
        PHP = 18, //Philippine peso
        PLN = 19, // Polish złoty
        RUB = 20, //Russian ruble
        SGD = 21, //Singapore dollar 
        SEK = 22, //  Swedish krona 
        CHF = 23, //   Swiss franc
        THB = 24, //  Thai baht
        USD = 25, //United States dollar
    }

    public enum DonationTypeEnum
    {
        [Display(Name = "None")]
        None = 1,

        [Display(Name = "Is Aggregate")]
        IsAggregate = 2,

        [Display(Name = "Is Anonymous")]
        IsAnonymous = 3
    }

    public enum DonationTransactionFeeMethod
    {
        GoCardless = 1,
        Braintree = 2,
        [Display(Name = "Pay A Charity")]
        Payacharity = 3,
        Paypal = 4,
    }

    public enum DonorSource
    {
        DonateNow = 1,
        App = 2,
        ContactLess = 3,
        PACDonateNow = 4,
        TrueLayer = 5,
        RegularDonation = 6,
        External = 7
    }
    public enum GocardLessAccountStatus
    {
        Successful = 0,
        InReview = 1,
        ActionRequired = 2
    }

    public enum GoCardLessFrequency
    {
        [Display(Name = "Weekly")]
        Weekly = 1,
        [Display(Name = "Monthly")]
        Monthly = 2,
        [Display(Name = "Yearly")]
        Yearly = 3,
    }
    public enum GoCardLessSubscriptionStatus
    {
        Confirmed = 1,
        Completed = 2,
    }
    public enum ExportDonor
    {
        [Display(Name = "All Donors")]
        AllDonors = 1,
        [Display(Name = "Donors in the following SmartFilters")]
        WithSmartFilter = 2,
        [Display(Name = "Donors who are tagged")]
        TaggedDonor = 3
    }

    public enum DateOptions
    {
        Ever = 1,
        Between = 2,
        [Display(Name = "In the last")]
        InTheLast = 3,
        [Display(Name = "In the previous financial year")]
        PreviousFinancialYear = 4,
        [Description("In the current financial year")]
        [Display(Name = "In the current financial year")]
        CurrentFinancialYear = 5,
        [Display(Name = "In the previous tax year")]
        PreviousTaxYear = 6,
        [Display(Name = "In the current tax year")]
        CurrentTaxYear = 7
    }
    public enum StringCondition
    {
        [Display(Name = "contains...")]
        Contains = 1,
        [Display(Name = "starts with...")]
        StartsWith = 2
    }
    public enum DateConditionDateDay
    {
        [Display(Name = "the date")]
        Date = 1,
        [Display(Name = "this number of days ago")]
        Day = 2
    }
    public enum DateConditionBeforeAfter
    {
        Before = 1,
        [Display(Name = "On or after")]
        After = 2,
        Empty = 3
    }
    public enum BlankDeclarationType
    {
        [Display(Name = "As well as the selected donors")]
        SelectedDonors,
        [Display(Name = "Instead of selecting any donors")]
        InstaedOfDonors
    }
    public enum DeclarationType
    {
        Current = 1,
        [Display(Name = "Not Current")]
        NotCurrent = 2,
        Expired = 3,
        [Display(Name = "Never Signed")]
        NeverSigned = 4
    }
    public enum UserAccount
    {
        [Display(Name = "Has a User Account")]
        HasUserAccount = 1,
        [Display(Name = "Does not have a User Account")]
        DontHaveUserAccount = 2
    }
    public enum FinancialYear
    {
        CurrentFinancialYearStartDate = 1,
        CurrentFinancialYearEndDate = 2,
        PreviousFinancialYearStartDate = 3,
        PreviousFinancialYearEndDate = 4
    }


    public enum GoCardlessPaymentStatus
    {
        //
        // Summary:
        //     Unknown status
        [Display(Name = "Unknown")]
        Unknown = 0,
        //
        // Summary:
        //     `status` with a value of "pending_customer_approval"
        [Display(Name = "Pending Customer Approval")]
        PendingCustomerApproval = 1,
        //
        // Summary:
        //     `status` with a value of "pending_submission"
        [Display(Name = "Pending Submission")]
        PendingSubmission = 2,
        //
        // Summary:
        //     `status` with a value of "submitted"
        [Display(Name = "Submitted")]
        Submitted = 3,
        //
        // Summary:
        //     `status` with a value of "confirmed"
        [Display(Name = "Confirmed")]
        Confirmed = 4,
        //
        // Summary:
        //     `status` with a value of "paid_out"
        [Display(Name = "Paid Out")]
        PaidOut = 5,
        //
        // Summary:
        //     `status` with a value of "cancelled"
        [Display(Name = "Cancelled")]
        Cancelled = 6,
        //
        // Summary:
        //     `status` with a value of "customer_approval_denied"
        [Display(Name = "Customer Approval Denied")]
        CustomerApprovalDenied = 7,
        //
        // Summary:
        //     `status` with a value of "failed"
        [Display(Name = "Failed")]
        Failed = 8,
        //
        // Summary:
        //     `status` with a value of "charged_back"
        [Display(Name = "Charged Back")]
        ChargedBack = 9
    }

    public enum GoCardLessResourceTypeAction
    {
        created = 1,
        customer_approval_granted = 2,
        customer_approval_denied = 3,
        submitted = 4,
        confirmed = 5,
        chargeback_cancelled = 6,
        paid_out = 7,
        late_failure_settled = 8,
        chargeback_settled = 9,
        surcharge_fee_credited = 10,
        surcharge_fee_debited = 11,
        failed = 12,
        charged_back = 13,
        cancelled = 14,
        resubmission_requested = 15,
        payment_created = 16,
        amended = 17,
        finished = 18
    }

    public enum GoCardlessResourceTypeCause
    {
        payment_created = 1,
        instalment_schedule_created = 2,
        customer_approval_granted = 3,
        customer_approval_denied = 4,
        payment_submitted = 5,
        payment_confirmed = 6,
        payment_paid_out = 7,
        late_failure_settled = 8,
        chargeback_settled = 9,
        surcharge_fee_credited = 10,
        surcharge_fee_debited = 11,
        refer_to_payer = 12,
        bank_account_closed = 13,
        invalid_bank_details = 14,
        authorisation_disputed = 15,
        other = 16,
        test_failure = 17,
        mandate_cancelled = 18,
        direct_debit_not_enabled = 19,
        insufficient_funds = 20,
        refund_requested = 21,
        instalment_schedule_cancelled = 22,
        payment_cancelled = 23,
        bank_account_transferred = 24,
        mandate_expired = 25,
        payment_retried = 26,
        payment_autoretried = 27,
        subscription_created = 28,
        subscription_cancelled = 29,
        plan_cancelled = 30,
        subscription_finished = 31,
        subscription_amended = 32
    }
    public enum MimeTypes
    {
        [Description("image/bmp")]
        bmp,
        [Description("image/gif")]
        gif,
        [Description("image/jp2")]
        jp2,
        [Description("image/jpeg")]
        jpe,
        [Description("image/jpeg")]
        jpeg,
        [Description("image/jpeg")]
        jpg,
        [Description("image/png")]
        png,
        [Description("application/pdf")]
        pdf,
        [Description("application/msword")]
        doc,
        [Description("text/csv")]
        csv,
        [Description("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        docx,
        [Description("application/vnd.ms-excel")]
        xls,
        [Description("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        xlsx,
        [Description("application/postscript")]
        ai,
        [Description("audio/x-aiff")]
        aif,
        [Description("audio/x-aiff")]
        aifc,
        [Description("audio/x-aiff")]
        aiff,
        [Description("text/plain")]
        asc,
        [Description("application/atom+xml")]
        atom,
        [Description("audio/basic")]
        au,
        [Description("video/x-msvideo")]
        avi,
        [Description("application/x-bcpio")]
        bcpio,
        [Description("application/octet-stream")]
        bin,
        [Description("application/x-netcdf")]
        cdf,
        [Description("image/cgm")]
        cgm,
        [Description("application/x-cpio")]
        cpio,
        [Description("application/mac-compactpro")]
        cpt,
        [Description("application/x-csh")]
        csh,
        [Description("text/css")]
        css,
        [Description("application/x-director")]
        dcr,
        [Description("video/x-dv")]
        dif,
        [Description("application/x-director")]
        dir,
        [Description("image/vnd.djvu")]
        djv,
        [Description("image/vnd.djvu")]
        djvu,
        [Description("application/octet-stream")]
        dll,
        [Description("application/octet-stream")]
        dmg,
        [Description("application/octet-stream")]
        dms,
        [Description("application/xml-dtd")]
        dtd,
        [Description("video/x-dv")]
        dv,
        [Description("application/x-dvi")]
        dvi,
        [Description("application/x-director")]
        dxr,
        [Description("application/postscript")]
        eps,
        [Description("text/x-setext")]
        etx,
        [Description("application/octet-stream")]
        exe,
        [Description("application/andrew-inset")]
        ez,
        [Description("application/srgs")]
        gram,
        [Description("application/srgs+xml")]
        grxml,
        [Description("application/x-gtar")]
        gtar,
        [Description("application/x-hdf")]
        hdf,
        [Description("application/mac-binhex40")]
        hqx,
        [Description("text/html")]
        htm,
        [Description("text/html")]
        html,
        [Description("x-conference/x-cooltalk")]
        ice,
        [Description("image/x-icon")]
        ico,
        [Description("text/calendar")]
        ics,
        [Description("image/ief")]
        ief,
        [Description("text/calendar")]
        ifb,
        [Description("model/iges")]
        iges,
        [Description("model/iges")]
        igs,
        [Description("application/x-java-jnlp-file")]
        jnlp,
        [Description("application/x-javascript")]
        js,
        [Description("audio/midi")]
        kar,
        [Description("application/x-latex")]
        latex,
        [Description("application/octet-stream")]
        lha,
        [Description("application/octet-stream")]
        lzh,
        [Description("audio/x-mpegurl")]
        m3u,
        [Description("audio/mp4a-latm")]
        m4a,
        [Description("audio/mp4a-latm")]
        m4b,
        [Description("audio/mp4a-latm")]
        m4p,
        [Description("video/vnd.mpegurl")]
        m4u,
        [Description("video/x-m4v")]
        m4v,
        [Description("image/x-macpaint")]
        mac,
        [Description("application/x-troff-man")]
        man,
        [Description("application/mathml+xml")]
        mathml,
        [Description("application/x-troff-me")]
        me,
        [Description("model/mesh")]
        mesh,
        [Description("audio/midi")]
        mid,
        [Description("audio/midi")]
        midi,
        [Description("application/vnd.mif")]
        mif,
        [Description("video/quicktime")]
        mov,
        [Description("video/x-sgi-movie")]
        movie,
        [Description("audio/mpeg")]
        mp2,
        [Description("audio/mpeg")]
        mp3,
        [Description("video/mp4")]
        mp4,
        [Description("video/mpeg")]
        mpe,
        [Description("video/mpeg")]
        mpeg,
        [Description("video/mpeg")]
        mpg,
        [Description("audio/mpeg")]
        mpga,
        [Description("application/x-troff-ms")]
        ms,
        [Description("model/mesh")]
        msh,
        [Description("video/vnd.mpegurl")]
        mxu,
        [Description("application/x-netcdf")]
        nc,
        [Description("application/oda")]
        oda,
        [Description("application/ogg")]
        ogg,
        [Description("image/x-portable-bitmap")]
        pbm,
        [Description("image/pict")]
        pct,
        [Description("chemical/x-pdb")]
        pdb,
        [Description("image/x-portable-graymap")]
        pgm,
        [Description("application/x-chess-pgn")]
        pgn,
        [Description("image/pict")]
        pic,
        [Description("image/pict")]
        pict,
        [Description("image/x-portable-anymap")]
        pnm,
        [Description("image/x-macpaint")]
        pnt,
        [Description("image/x-macpaint")]
        pntg,
        [Description("image/x-portable-pixmap")]
        ppm,
        [Description("application/vnd.ms-powerpoint")]
        ppt,
        [Description("application/postscript")]
        ps,
        [Description("video/quicktime")]
        qt,
        [Description("image/x-quicktime")]
        qti,
        [Description("image/x-quicktime")]
        qtif,
        [Description("audio/x-pn-realaudio")]
        ra,
        [Description("audio/x-pn-realaudio")]
        ram,
        [Description("image/x-cmu-raster")]
        ras,
        [Description("application/rdf+xml")]
        rdf,
        [Description("image/x-rgb")]
        rgb,
        [Description("application/vnd.rn-realmedia")]
        rm,
        [Description("application/x-troff")]
        roff,
        [Description("text/rtf")]
        rtf,
        [Description("text/richtext")]
        rtx,
        [Description("text/sgml")]
        sgm,
        [Description("text/sgml")]
        sgml,
        [Description("application/x-sh")]
        sh,
        [Description("application/x-shar")]
        shar,
        [Description("model/mesh")]
        silo,
        [Description("application/x-stuffit")]
        sit,
        [Description("application/x-koan")]
        skd,
        [Description("application/x-koan")]
        skm,
        [Description("application/x-koan")]
        skp,
        [Description("application/x-koan")]
        skt,
        [Description("application/smil")]
        smi,
        [Description("application/smil")]
        smil,
        [Description("audio/basic")]
        snd,
        [Description("application/octet-stream")]
        so,
        [Description("application/x-futuresplash")]
        spl,
        [Description("application/x-wais-source")]
        src,
        [Description("application/x-sv4cpio")]
        sv4cpio,
        [Description("application/x-sv4crc")]
        sv4crc,
        [Description("image/svg+xml")]
        svg,
        [Description("application/x-shockwave-flash")]
        swf,
        [Description("application/x-troff")]
        t,
        [Description("application/x-tar")]
        tar,
        [Description("application/x-tcl")]
        tcl,
        [Description("application/x-tex")]
        tex,
        [Description("application/x-texinfo")]
        texi,
        [Description("application/x-texinfo")]
        texinfo,
        [Description("image/tiff")]
        tif,
        [Description("image/tiff")]
        tiff,
        [Description("application/x-troff")]
        tr,
        [Description("text/tab-separated-values")]
        tsv,
        [Description("text/plain")]
        txt,
        [Description("application/x-ustar")]
        ustar,
        [Description("application/x-cdlink")]
        vcd,
        [Description("model/vrml")]
        vrml,
        [Description("application/voicexml+xml")]
        vxml,
        [Description("audio/x-wav")]
        wav,
        [Description("image/vnd.wap.wbmp")]
        wbmp,
        [Description("application/vnd.wap.wbxml")]
        wbmxl,
        [Description("text/vnd.wap.wml")]
        wml,
        [Description("application/vnd.wap.wmlc")]
        wmlc,
        [Description("text/vnd.wap.wmlscript")]
        wmls,
        [Description("application/vnd.wap.wmlscriptc")]
        wmlsc,
        [Description("model/vrml")]
        wrl,
        [Description("image/x-xbitmap")]
        xbm,
        [Description("application/xhtml+xml")]
        xht,
        [Description("application/xhtml+xml")]
        xhtml,
        [Description("application/xml")]
        xml,
        [Description("image/x-xpixmap")]
        xpm,
        [Description("application/xml")]
        xsl,
        [Description("application/xslt+xml")]
        xslt,
        [Description("application/vnd.mozilla.xul+xml")]
        xul,
        [Description("image/x-xwindowdump")]
        xwd,
        [Description("chemical/x-xyz")]
        xyz,
        [Description("application/zip")]
        zip
    }

    public enum ShippingMethod
    {
        [Description("Best Way")]
        tablerate = 1,
        [Description("Free Shipping")]
        freeshipping = 2,
        [Description("Federal Express")]
        fedex = 3,
        [Description("DHL")]
        dhl = 4,
        [Description("United Parcel Service")]
        ups = 5,
        [Description("United States Postal Service")]
        usps = 6
    }

}
