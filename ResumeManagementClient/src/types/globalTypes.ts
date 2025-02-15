export interface ICompany{
    id: number,
    name: string,
    size: string,
    createdAt:string,
    updatedAt:string,
    isActive:boolean,
};

export interface ICreateCompanyDTO{
    name: string,
    size: string,
};

export type createCompanyError ={
    property: string,
    message: string,
}