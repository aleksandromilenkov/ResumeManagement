export interface IBase{
    id: number,
    createdAt:string,
    updatedAt:string,
    isActive:boolean,
}
export interface ICompany extends IBase{
    name: string,
    size: string,
};

export interface ICreateCompanyDTO{
    name: string,
    size: string,
};

export type createEntityError ={
    property: string,
    message: string,
}

export interface IJob extends IBase{
    title:string,
    companyId: string,
    companyName: string,
    level: string
}

export interface ICreateJobDTO{
    title: string,
    companyId: number,
    level: string
};

export interface ICandidate extends IBase{
    firstName:string,
    lastName: string,
    email: string,
    phone: string
    coverLetter: string
    resumeUrl: string
    jobTitle: string
    jobId:number
}

export interface ICreateCandidateDTO{
    firstName:string,
    lastName: string,
    email: string,
    phone: string
    coverLetter: string
    resumeUrl: File | null
    jobId:number | null
}
/*
 public string FirstName { get; set; }
 public string LastName { get; set; }
 public string Email { get; set; }
 public string Phone { get; set; }
 public string CoverLetter { get; set; }
 public string ResumeUrl { get; set; }
 public string JobTitle { get; set; }
 public int JobId { get; set; }
*/