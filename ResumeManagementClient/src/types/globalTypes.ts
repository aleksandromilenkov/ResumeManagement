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

export type createCompanyError ={
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