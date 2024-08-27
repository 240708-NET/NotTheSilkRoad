import loadstyles from './WaveLoading.module.css'
import styled, { keyframes } from 'styled-components';


const commonStyle = {
    margin: 'auto',
    position: 'relative',
    left: 0,
    right: 0,
    top: 0,
    bottom: 0
  };
  
  const sizeItem = {
    small: '10px',
    default: '12px',
    large: '14px'
  }
  
  const sizeContainer = {
    small: '24px',
    default: '30px',
    large: '36px'
  }
  
  const borderRadiusContainerSize = {
    small: '12px',
    default: '15px',
    large: '18px'
  }

const stretchdelay = keyframes`
  0%,
  40%,
  100% {
    -webkit-transform: scaleY(0.4);
  }
  20% {
    -webkit-transform: scaleY(1);
  }
`;

const LoadContainer = styled.div`
  width: 100px;
  height: ${props => sizeContainer[props.size] || sizeContainer['default']};
  text-align: center;
  font-size: 10px;
`;

const box = styled.div`
  background-color: ${props => props.color || '#00adb5'};
  height: 100%;
  width: 6px;
  display: inline-block;
  margin-left: 5px;
  animation: ${stretchdelay} ${props => props.speed || 1.2}s infinite ease-in-out;
`;

const BoxLoadingFirst = styled(box)`
  animation-delay: -1.2s;
`;

const BoxLoadingTwo = styled(box)`
  animation-delay: -1.1s;
`;

const BoxLoadingThree = styled(box)`
  animation-delay: -1s;
`;

const BoxLoadingFour = styled(box)`
  animation-delay: -0.9s;
`;

const BoxLoadingFive = styled(box)`
  animation-delay: -0.8s;
`;

const WaveLoading = ({ style = commonStyle, color, speed, size="default" }) => {
  return (
    <div className={loadstyles.main}>
    <LoadContainer style={style} size={size}>
      <BoxLoadingFirst color={color} speed={speed} />
      <BoxLoadingTwo color={color} speed={speed} />
      <BoxLoadingThree color={color} speed={speed} />
      <BoxLoadingFour color={color} speed={speed} />
      <BoxLoadingFive color={color} speed={speed} />
    </LoadContainer>
    </div>
  );
};

export default WaveLoading;