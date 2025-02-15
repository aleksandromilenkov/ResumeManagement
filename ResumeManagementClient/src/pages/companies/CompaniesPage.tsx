import React, { useEffect, useState } from 'react'
import httpModule from '../../utils/axiosHelper'
import { ICompany } from '../../types/globalTypes';
import Spinner from '../../utils/Spinner';
import { AxiosError } from 'axios';
import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { Add } from '@mui/icons-material';
import Companies from '../../components/Companies/Companies';
import "./companiesPage.scss";

type Props = {}

const CompaniesPage = (props: Props) => {
  const [companies, setCompanies] = useState<ICompany[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<AxiosError | any>();
  const navigate = useNavigate();
  console.log(error);
  useEffect(()=>{
    const fetchCompanies = async () => {
        try {
          setIsLoading(true);
          const response = await httpModule.get("/company");
          setCompanies(response.data);
        } catch (err) {
          setError(err);
        } finally {
          setIsLoading(false);
        }
      };
    
      fetchCompanies();
  },[])
  if(isLoading) return <Spinner/>
  if(error) return <p>{error.message}</p>
  return (
    <div className='content companies'>
        <div className="heading">
            <h2>Companies</h2>
            <Button variant='outlined' onClick={()=> navigate("/companies/add")}>
                <Add/>
            </Button>
        </div>
        <Companies companies={companies}/>
    </div>
  )
}

export default CompaniesPage