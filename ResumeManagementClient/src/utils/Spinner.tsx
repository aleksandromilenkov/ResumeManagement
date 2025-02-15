import { Box, LinearProgress } from '@mui/material'
import React from 'react'

type Props = {}

const Spinner = (props: Props) => {
  return (
    <div> <Box sx={{ width: '100%' }}>
    <LinearProgress />
  </Box></div>
  )
}

export default Spinner